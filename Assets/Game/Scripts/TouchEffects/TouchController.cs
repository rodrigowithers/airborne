using System;
using deJex;
using UnityEngine;

namespace Game.Scripts.TouchEffects
{
    public class TouchController : MonoBehaviour, ITouchController
    {
        private Camera _mainCamera;
        
        public event Action<Vector3> OnTouchDown = (pos) => { };
        public event Action<Vector3> OnTouch = (pos) => { };
        public event Action<Vector3> OnDrag = (pos) => { };

        private Vector3 _newPosition;
        private Vector3 _initialTouchPosition;
        
        private float _dragTreshold = 0.1f;

        private float _currenDragDistance;

        private void Awake()
        {
            Container.BindSingleton<ITouchController>(this);
        }

        private void Start()
        {
            // Cached for extra performance
            _mainCamera = Camera.main;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _newPosition.z = 0;

                _initialTouchPosition = _newPosition;
                
                OnTouchDown.Invoke(_newPosition);
            }
            
            if (Input.GetMouseButton(0))
            {
                _newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _newPosition.z = 0;

                OnTouch.Invoke(_newPosition);
                
                _currenDragDistance = Vector3.Distance(_initialTouchPosition, _newPosition);
                if (_currenDragDistance > _dragTreshold)
                {
                    OnDrag.Invoke(_newPosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _currenDragDistance = 0;
            }
        }
    }
}