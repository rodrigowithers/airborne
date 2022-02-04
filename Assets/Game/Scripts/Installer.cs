using deJex;
using Game.Scripts.Ads;
using UnityEngine;

namespace Game.Scripts
{
    [DefaultExecutionOrder(int.MinValue + 1)]
    public static class Installer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            #if UNITY_EDITOR
            Container.Bind<IGetAdTestMode>().To(new EditorAdTestMode());
            #elif UNITY_ANDROID
            Container.Bind<IGetAdTestMode>().To(new LiveAdTestMode());
            #endif
        }
    }
}