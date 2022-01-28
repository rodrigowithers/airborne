using System;
using UnityEngine;

namespace Game.Scripts.TouchEffects
{
    public interface ITouchController
    {
        public event Action<Vector3> OnTouchDown;
        public event Action<Vector3> OnTouch;
        public event Action<Vector3> OnDrag;
    }
}