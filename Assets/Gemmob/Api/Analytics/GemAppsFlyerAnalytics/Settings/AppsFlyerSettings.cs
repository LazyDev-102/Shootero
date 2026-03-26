using UnityEngine;
using System.Collections;

namespace Gemmob.Api.Analytics {
    [CreateAssetMenu(fileName = "AppsFlyerSettings", menuName = "Gemmob/Api/Analytics/AppsFlyerSettings")]
    public class AppsFlyerSettings : ScriptableObject {
        [SerializeField] string devKey;
        [SerializeField][Tooltip("Android Package Name")] string appId_Android;
        [SerializeField][Tooltip("iOS ID as number")] string appId_iOS;

        public string DevKey => devKey;

        public string AppID {
            get {
#if UNITY_ANDROID
                return appId_Android;
#else
                return appId_iOS;
#endif
            }
        }
    }
}