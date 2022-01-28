using UnityEngine;

namespace Game.Scripts.Boss
{
    public class ShieldPiece : MonoBehaviour, IBreakeable
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color[] _colors;
        
        public int LifePoints { get; set; } = 3;

        public void TakeDamage(int damage)
        {
            LifePoints -= damage;
            _renderer.color = _colors[3 - LifePoints];
            
            if (LifePoints <= 0)
            {
                Break();
            }
        }

        public void Break()
        {
            Destroy(this.gameObject);
        }
    }
}
