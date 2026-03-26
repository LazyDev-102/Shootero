#if APPSFLYER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

namespace Gemmob.Api.Analytics {
    /// <summary>
    /// Current SDK: AppsFlyer v5.3.0 https://github.com/AppsFlyerSDK/appsflyer-unity-plugin
    /// </summary>
    public class GemAppsFlyerAnalytics : Singleton<GemAppsFlyerAnalytics> {
 
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Gemmob/Api/Analytics/AppsFlyerSettings")]
        public static void SelectSettings() {
            UnityEditor.Selection.activeObject = Resources.Load<AppsFlyerSettings>("AppsFlyer/AppsFlyerSettings");
        }
#endif

        protected override void Initialize() {
            Init();
        }

        private void Init() {
            var settings = Resources.Load<AppsFlyerSettings>("AppsFlyer/AppsFlyerSettings");
            if (settings == null) {
                Logs.LogError("[APPSFLYER] Cannot load setting from resources path: AppsFlyer/AppsFlyerSettings");
                return;
            }

            AppsFlyer.setIsDebug(Logs.IsEnable);

            AppsFlyer.initSDK(settings.DevKey, settings.AppID);
            AppsFlyer.startSDK();
            Logs.Log($"<color=green>[APPSFLYER] Initialized: SDK Version={AppsFlyer.getSdkVersion()}</color>");
        }

        public void LogEvent(string eventName) {
            this.Log(eventName, null);
        }

        public void LogEvent(string eventName, string paraName, string paraValue) {
            this.Log(eventName, new Dictionary<string, string>() { { paraName, paraValue } });
        }

        public void LogEvent(string eventName, ParameterBuilder parameterBuilder) {
            this.Log(eventName, parameterBuilder != null ? parameterBuilder.BuildDictString() : null);
        }

        public void Log(string eventName, Dictionary<string, string> para) {
            Logs.Log($"[APPSFLYER] eventName={eventName}, paraCount={(para != null ? para.Count : 0)}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            AppsFlyer.sendEvent(eventName, para);
        }
    }
}
#endif