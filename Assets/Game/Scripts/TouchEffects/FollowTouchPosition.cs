using deJex;
using UnityEngine;

namespace Game.Scripts.TouchEffects
{
    public class FollowTouchPosition : MonoBehaviour
    {
        private Transform _transform;

        private ITouchController _touchController;

        private void Start()
        {
            // Cached transform for extra performance
            _transform = transform;

            _touchController = Container.Resolve<ITouchController>();
            _touchController.OnTouch += OnTouch;
        }

        private void OnTouch(Vector3 position)
        {
            _transform.position = position;
        }
    }
}