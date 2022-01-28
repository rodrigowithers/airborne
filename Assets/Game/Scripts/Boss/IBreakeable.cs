namespace Game.Scripts.Boss
{
    public interface IBreakeable
    {
        int LifePoints { get; set; }
        
        void TakeDamage(int damage);
        void Break();
    }
}