using deJex;
using Player;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class PlayerBounceVisualizer : MonoBehaviour
    {
        public RectTransform BounceCooldownBar;

        private IPlayer _player;

        private void Start()
        {
            _player = Container.Resolve<IPlayer>();
        }

        private void Update()
        {
            if (_player.Equals(null))
            {
                _player = Container.Resolve<IPlayer>();
            }
            
            float scale = 1 - Mathf.Clamp01(_player.BounceCooldown / _player.BounceDelay);
            BounceCooldownBar.localScale = new Vector3(scale, 1, 1);
        }
    }
}