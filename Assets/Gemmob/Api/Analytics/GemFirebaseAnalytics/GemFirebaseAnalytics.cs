
#if FIREBASE
using Firebase.Analytics;
using Gemmob.Api.CSFirebases;

namespace Gemmob.Api.Analytics {
    public class GemFirebaseAnalytics : Singleton<GemFirebaseAnalytics> {
        private bool available;
        private event System.Action callOnAvailable;

        protected override void Initialize() {
            available = false;
            GemFirebase.Instance.InitModule(InitAnalytics);
        }

        private void InitAnalytics() {
            if (available) return;
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            available = true;
            if (callOnAvailable != null) {
                callOnAvailable.Invoke();
                callOnAvailable = null;
            }
        }

        private void DebugLog(string msg) {
            Logs.Log($"[FIREBASE] {msg}");
        }

        #region Push Log Events
        public void SetUserProperty(string name, string property) {
            DebugLog($"UserProperty: name={name}, property={property}");
            Push(() => { FirebaseAnalytics.SetUserProperty(name, property); });
        }

        public void LogEvent(string eventName, string paraName, string paraValue) {
            DebugLog($"Event: {eventName} [{paraName}:{paraValue}]");
            Push(() => { FirebaseAnalytics.LogEvent(eventName, paraName, paraValue); });
        }

        public void LogEvent(string eventName, string paraName, int paraValue) {
            DebugLog($"Event: {eventName} [{paraName}:{paraValue}]");
            Push(() => { FirebaseAnalytics.LogEvent(eventName, paraName, paraValue); });
        }

        public void LogEvent(string eventName, string paraName, long paraValue) {
            DebugLog($"Event: {eventName} [{paraName}:{paraValue}]");
            Push(() => { FirebaseAnalytics.LogEvent(eventName, paraName, paraValue); });
        }

        public void LogEvent(string eventName, string paraName, double paraValue) {
            DebugLog($"Event: {eventName} [{paraName}:{paraValue}]");
            Push(() => { FirebaseAnalytics.LogEvent(eventName, paraName, paraValue); });
        }

        public void LogEvent(string eventName, params Firebase.Analytics.Parameter[] para) {
            DebugLog($"Event: {eventName}, paraCount={(para != null ? para.Length : 0)}");
            Push(() => {
                if (para == null) FirebaseAnalytics.LogEvent(eventName);
                else FirebaseAnalytics.LogEvent(eventName, para);
            });
        }

        public void LogEvent(string eventName, ParameterBuilder builder) {
            LogEvent(eventName, builder.BuildFirebase());
        }

        private void Push(System.Action action) {
            if (UnityEngine.Application.isEditor) {
                return;
            }
            try {
                if (available) action.Invoke();
                else callOnAvailable += action;
            }
            catch { throw; }
        }
        #endregion
    }

    public partial class ParameterBuilder {
        public Firebase.Analytics.Parameter[] BuildFirebase() {
            var para = new Firebase.Analytics.Parameter[parameters.Count];
            int idx = 0;
            foreach (var item in parameters) {
                para[idx] = new Firebase.Analytics.Parameter(item.Key, item.Value.ToString());
                idx++;
            }

            return para;
        }
    }
}

#endif