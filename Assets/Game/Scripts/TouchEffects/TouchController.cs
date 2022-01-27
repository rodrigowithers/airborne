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
        
        private Vector3 _newPosition;

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
                
                OnTouchDown.Invoke(_newPosition);
            }
            
            if (Input.GetMouseButton(0))
            {
                _newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _newPosition.z = 0;

                OnTouch.Invoke(_newPosition);
            }
        }
    }
}