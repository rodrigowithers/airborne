using deJex;
using UnityEngine;

namespace Game.Scripts.TouchEffects
{
    public class TouchTrail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        
        private ITouchController _touchController;

        private void Start()
        {
            _touchController = Container.Resolve<ITouchController>();

            _touchController.OnTouchDown += OnTouchDown;
            _touchController.OnTouch += OnTouch;
        }

        private void OnTouch(Vector3 position)
        {
            _trailRenderer.emitting = true;
        }

        private void OnTouchDown(Vector3 position)
        {
            _trailRenderer.emitting = false;
        }
    }
}