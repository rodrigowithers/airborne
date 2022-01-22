using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;

        [SerializeField] private float KickForce = 25;

        private Camera _main;

        private Vector2 _initialPos;
        private Vector2 _currentPos;

        private bool _drag;

        private void Bounce(Vector2 position)
        {
            if (_body.isKinematic)
                _body.isKinematic = false;
            
            Vector2 dir = ((Vector2) transform.position - position).normalized;

            _body.velocity = Vector2.up * Random.Range(5, 6) + Vector2.right * dir.x;
            _body.angularVelocity = -dir.x * 100;
        }

        private void Kick(Vector2 direction)
        {
            _body.velocity = direction * KickForce;
        }

        private void Start()
        {
            _main = Camera.main;

            _body.isKinematic = true;
        }

        private void Update()
        {
            Vector2 mousePos = _main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.CircleCast(mousePos, 0.5f, Vector2.up, 0.1f);

                if (hit.collider != null)
                    Bounce(mousePos);
                else
                {
                    _drag = true;
                    _initialPos = mousePos;
                }
            }

            if (Input.GetMouseButton(0) && _drag)
            {
                _currentPos = mousePos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!_drag) return;

                _drag = false;

                var distance = (_currentPos - _initialPos);
                var mag = distance.magnitude;
                if (mag <= 1) return;

                var dir = distance.normalized;


                var hit = Physics2D.CircleCast(_initialPos, 1.0f, dir, mag);

                Debug.DrawRay(_initialPos, dir * mag, Color.red, 3);

                if (hit.collider != null)
                {
                    Kick(dir);
                }
            }
        }
    }
}