using UnityEngine;

namespace Game.Spring
{
    public class Spring : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int TriggerHash = Animator.StringToHash("Activate");

        public void Trigger()
        {
            _animator.SetTrigger(TriggerHash);
        }
    }
}