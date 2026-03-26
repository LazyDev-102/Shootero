#if GA

using GameAnalyticsSDK;
using System.Diagnostics;

namespace Gemmob.Api.Analytics {
    public class GemGameAnalytics : SingletonFreeAlive<GemGameAnalytics> {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Gemmob/Api/Analytics/GameAnalytics Settings")]
        public static void SelectSettings() {
            UnityEditor.Selection.activeObject = UnityEngine.Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");
        }
#endif

        protected override void OnAwake() {
            gameObject.AddComponent<GameAnalyticsSDK.Events.GA_SpecialEvents>();
            gameObject.AddComponent<GameAnalyticsSDK.GameAnalytics>();
        }

        /// <summary> [Optional] must call before Init/Preload </summary>
        public static void SetUserID(string userID) {
            if (GemGameAnalytics.Initialized) {
                GameAnalytics.SetCustomId(userID);
            }
        }

        /// <summary> You must call this Preload before use log event.</summary>
        public override void Preload() {
            GameAnalytics.Initialize();
        }

        public void LogEvent(string eventName) {
            Logs.Log($"[GA_EVENTS] eventName={eventName}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            GameAnalytics.NewDesignEvent(eventName);
        }

        public void LogEvent(string eventName, float value) {
            Logs.Log($"[GA_EVENTS] eventName={eventName}, value={value}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            GameAnalytics.NewDesignEvent(eventName, value);
        }

        public void LogEvent(string eventName, ParameterBuilder builder) {
            Logs.Log($"[GA_EVENTS] eventName={eventName}, paraCount={builder.BuildDictObject().Count}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            GameAnalyticsSDK.Events.GA_Design.NewEvent(eventName, builder.BuildDictObject());
        }
    }
}
#endif
