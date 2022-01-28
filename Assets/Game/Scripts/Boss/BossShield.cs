using UnityEngine;

namespace Game.Scripts.Boss
{
    public class BossShield : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;

        private void Update()
        {
            transform.Rotate(Vector3.forward, _speed);
        }
    }
}
