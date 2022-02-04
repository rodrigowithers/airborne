using UnityEngine;
using UnityEngine.Advertisements;

namespace Game.Scripts.Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        
#if UNITY_ANDROID
        private static string _gameId = "4595528";
#elif UNITY_IOS
        private static string _gameId = "4595529";
#endif

        [SerializeField] private AdsManager _manager;
        
        private static bool _testMode = true;

        private void Awake()
        {
            // Initialize Ads
            Debug.Log("[ Ads ] Initializing...");
            Advertisement.Initialize(_gameId, _testMode);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("[ Ads ] Ads Initialized!");
            _manager.LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"[ Ads ] Initialization failed: {error.ToString()} - {message}");
        }
    }
}