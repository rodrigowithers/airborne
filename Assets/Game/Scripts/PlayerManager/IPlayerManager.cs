using System;

namespace Game.Scripts.PlayerManager
{
    public interface IPlayerManager
    {
        public Action OnPlayerDied { get; set; }
        
        bool PlayerDead { get; set; }
        public void Respawn();
    }
}