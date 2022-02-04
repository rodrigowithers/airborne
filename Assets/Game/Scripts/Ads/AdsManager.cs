using System;
using deJex;
using Game.Scripts.Player;
using Game.Scripts.PlayerManager;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Game.Scripts.Ads
{
    public class AdsManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        
#if UNITY_ANDROID
        private string _unitId = "Interstitial_Android";
#elif UNITY_IOS
    private string _unitId = "Interstitial_iOS";
#endif            
        
        [ContextMenu("Show Ad")]
        public void ShowAd()
        {
            Debug.Log($"[ Ads ] Loading Ad: {_unitId}");
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

        public void OnUnityAdsShowStart(string placementId) { }
        public void OnUnityAdsShowClick(string placementId) { }
        public void OnUnityAdsAdLoaded(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("[ Ads ] Ad Completed!");
            Container.Resolve<IPlayerManager>().Respawn();
        }

        private void Start()
        {
            var playerManger = Container.Resolve<IPlayerManager>();
            playerManger.OnPlayerDied += ShowAd;
        }
    }
}