using deJex;
using UnityEngine;
using System.Collections;
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
        [SerializeField] private GameObject _toActivate;

        private static bool _testMode => Container.Resolve<IGetAdTestMode>().GetAdTestMode();

        public void OnInitializationComplete()
        {
            Debug.Log("[ Ads ] Ads Initialized!");
            // _manager.LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"[ Ads ] Initialization failed: {error.ToString()} - {message}");
        }
        
        private IEnumerator Start()
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode);
                Debug.Log($"[ Ads ] Initializing with TestMode = {_testMode}");
            }

            // Wait until Unity Ads is initialized
            while (!Advertisement.isInitialized)
            {
                Debug.Log($"[ Ads ] ... ");
                yield return new WaitForEndOfFrame();
            }
            
            Debug.Log($"[ Ads ] Initialized! ");

            _toActivate.SetActive(true);
            _manager.LoadAd();
        }
    }
}