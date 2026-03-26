#if ADJUST

using UnityEngine;
using com.adjust.sdk;
using System.Linq.Expressions;

namespace Gemmob.Api.Analytics {
    public class GemAdjustAnalytics : SingletonFreeAlive<GemAdjustAnalytics> {
                
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Gemmob/Api/Analytics/AdjustSettings")]
        public static void SelectSettings() {
            UnityEditor.Selection.activeObject = Resources.Load<AdjustSettings>("Adjust/AdjustSettings");
        }
#endif

        protected override void OnAwake() {
            var settings = Resources.Load<AdjustSettings>("Adjust/AdjustSettings");
            if (settings == null) {
                Logs.LogError("[ADJUST] Cannot load setting from resources path: Adjust/AdjustSettings");
                return;
            }

            if (string.IsNullOrEmpty(settings.AppToken)) {
                Logs.LogError("[ADJUST] You need to fill the setting at resources path: Adjust/AdjustSettings");
                return;
            }

            AdjustConfig config = new AdjustConfig(settings.AppToken, 
                                                    Logs.IsEnable ? AdjustEnvironment.Sandbox : AdjustEnvironment.Production, 
                                                    !Logs.IsEnable);
            config.setLogLevel(Logs.IsEnable ? AdjustLogLevel.Verbose : AdjustLogLevel.Suppress);
            config.setLogDelegate(msg => { UnityEngine.Debug.Log($"[ADJUST] {msg}"); });

            var adj = gameObject.AddComponent<Adjust>();
            adj.startManually = true;
            Adjust.start(config);
        }

        public void LogEvent(string eventName) {
            Logs.Log($"[ADJUST] eventName={eventName}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            Adjust.trackEvent(new AdjustEvent(eventName));
        }

        public void LogEvent(string eventName, double revenueAmount, string currency) {
            Logs.Log($"[ADJUST] eventName={eventName}, revenue={revenueAmount}, currency={currency}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            AdjustEvent ev = new AdjustEvent(eventName);
            ev.setRevenue(revenueAmount, currency);
            Adjust.trackEvent(ev);
        }

        public void LogEvent(string eventName, ParameterBuilder builder) {
            if (builder == null) {
                LogEvent(eventName);
                return;
            }

            Logs.Log($"[ADJUST] eventName={eventName}, paraCount={builder.BuildDictObject().Count}");
            if (UnityEngine.Application.isEditor) {
                return;
            }

            Adjust.trackEvent(builder.BuildAdjust(eventName));
        }

    }

    
    public partial class ParameterBuilder {
        public AdjustEvent BuildAdjust(string evenNameToken) {
            AdjustEvent adj = new AdjustEvent(evenNameToken);
            foreach (var item in parameters) {
                adj.addCallbackParameter(item.Key, item.Value.ToString());
            }

            return adj;
        }
    }
}
#endif