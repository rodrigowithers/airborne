using deJex;
using UnityEngine;
using Game.Scripts.PlayerManager;
using UnityEngine.Advertisements;

namespace Game.Scripts.Ads
{
    public class AdsManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        
#if UNITY_ANDROID
        private string _unitId = "Rewarded_Android";
#elif UNITY_IOS
    private string _unitId = "Interstitial_iOS";
#endif

        private IPlayerManager _playerManager;
        private bool _respawnPlayer;
        
        public void OnUnityAdsShowStart(string placementId) { }
        public void OnUnityAdsShowClick(string placementId) { }

        [ContextMenu("Show Ad")]
        public void ShowAd()
        {
            Debug.Log($"[ Ads ] Showing Ad: {_unitId}");
            Advertisement.Show(_unitId, this);            
        }
        
        public void LoadAd()
        {
            Debug.Log($"[ Ads ] Loading Ad: {_unitId}");
            Advertisement.Load(_unitId, this);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
        }
        
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {placementId} - {error.ToString()} - {message}");
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"[ Ads ] Loaded Ad Unit: {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("[ Ads ] Ad Completed!");
            _respawnPlayer = true;
        }

        private void Start()
        {
            _playerManager = Container.Resolve<IPlayerManager>();
            _playerManager.OnPlayerDied += ShowAd;
        }

        private void Update()
        {
            if (_respawnPlayer)
            {
                Debug.Log("Respawning Player");
                
                _respawnPlayer = false;
                _playerManager.Respawn();

                LoadAd();
            }
        }
    }
}