using System;
using deJex;
using UnityEngine;

namespace Game.Scripts.PlayerManager
{
    public class PlayerManager : MonoBehaviour, IPlayerManager
    {
        [SerializeField] private GameObject PlayerPrefab;
        public bool PlayerDead { get; set; }

        public Action OnPlayerDied { get; set; }

        public void Respawn()
        {
            PlayerDead = false;
            Instantiate(PlayerPrefab, new Vector3(0, -2.5f, 0), Quaternion.identity);
        }

        private void Awake()
        {
            Container.BindSingleton<IPlayerManager>(this);
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0) && PlayerDead)
            // {
            //     Respawn();
            // }
        }
    }
}