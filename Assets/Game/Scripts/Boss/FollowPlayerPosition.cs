using deJex;
using UnityEngine;
using Game.Scripts.Player;

namespace Game.Scripts.Boss
{
    public class FollowPlayerPosition : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private float _radius = 1;

        private IPlayerTransformationStorage _playerTransform;

        private void Start()
        {
            _playerTransform = Container.Resolve<IPlayerTransformationStorage>();
        }

        private void Update()
        {
            if (_playerTransform == null)
                _playerTransform = Container.Resolve<IPlayerTransformationStorage>();

            var dir = (_playerTransform.GetPlayerPosition() - _body.position).normalized;
            transform.position = _body.position + dir * _radius;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(_body.transform.position, _radius);
        }
    }
}