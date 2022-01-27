using deJex;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour, IPlayer, IPlayerTransformationStorage
    {
        [SerializeField] private Rigidbody2D _body;

        [SerializeField] private float KickForce = 25;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _damageLayer;
        [SerializeField] private LayerMask _enemyLayer;

        [SerializeField] private ParticleSystem _bounceParticles;
        [SerializeField] private GameObject _deathParticles;

        private Camera _main;

        private Vector2 _initialPos;
        private Vector2 _currentPos;

        private Vector2 _kickDirection;

        private bool _drag;
        private bool _kicking;
        
        [field: SerializeField] public float BounceDelay { get; set; } = 0.5f;
        public float BounceCooldown { get; set; }

        Vector3 IPlayerTransformationStorage.PlayerPosition
        {
            get
            {
                if (this == null)
                    return Vector3.zero;
                
                return transform.position;
            }
        }

        private void Bounce(Vector2 position)
        {
            if (_body.isKinematic)
                _body.isKinematic = false;

            _bounceParticles.transform.position = position;
            _bounceParticles.Emit(Random.Range(10, 30));

            Vector2 dir = ((Vector2) transform.position - position).normalized;

            _body.velocity = Vector2.up * Random.Range(5, 6) + Vector2.right * dir.x;
            _body.angularVelocity = -dir.x * 100;

            BounceCooldown = BounceDelay;
        }

        private void Kick(Vector2 direction)
        {
            if (_body.isKinematic)
                return;

            _body.velocity = direction * KickForce;

            _kicking = true;
            _kickDirection = direction;
        }

        private void Die()
        {
            Instantiate(_deathParticles, transform.position, Quaternion.identity);
            Container.Resolve<IPlayerManager>().PlayerDead = true;

            Destroy(this.gameObject);
        }

        private void Awake()
        {
            Container.BindSingleton<IPlayer>(this);
            Container.BindSingleton<IPlayerTransformationStorage>(this);
        }

        private void Start()
        {
            _main = Camera.main;
            _body.isKinematic = true;

            BounceCooldown = 0;
        }

        private void Update()
        {
            Vector2 mousePos = _main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.CircleCast(mousePos, 0.5f, Vector2.up, 0f, _playerLayer);
                DebugExtension.DebugCircle(mousePos, Vector3.forward, Color.red, 0.5f, 1);

                if (hit.collider != null && BounceCooldown <= 0)
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


                var hit = Physics2D.CircleCast(_initialPos, 0.5f, dir, mag, _playerLayer);

                Debug.DrawRay(_initialPos, dir * mag, Color.red, 3);

                if (hit.collider != null)
                {
                    Kick(dir);
                }
            }

            if (_body.velocity.y < 0)
            {
                _body.velocity += Vector2.down * 0.1f;
            }

            if (_kicking)
            {
                var hit = Physics2D.Raycast(transform.position, _kickDirection, 0.5f, _enemyLayer);
                DebugExtension.DebugArrow(transform.position, _kickDirection * 0.5f, Color.red);

                if (hit.collider != null)
                {
                    _kicking = false;
                    // Deal damage to enemy!
                }
            }

            BounceCooldown -= Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _kicking = false;

            if (_damageLayer == 1 << other.gameObject.layer)
            {
                Die();
            }
        }
    }
}