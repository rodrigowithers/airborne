using deJex;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.PlayerManager
{
    public class PlayerManager : MonoBehaviour, IPlayerManager
    {
        public bool PlayerDead { get; set; }

        [SerializeField] private GameObject PlayerPrefab;
        
        private void Awake()
        {
            Container.BindSingleton<IPlayerManager>(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && PlayerDead)
            {
                PlayerDead = false;

                Instantiate(PlayerPrefab, new Vector3(0, -2.5f, 0), Quaternion.identity);
            }
        }
    }
}