#if FACEBOOK

using System.Collections.Generic;

using Gemmob.Api.Facebooks;
using Facebook.Unity;

namespace Gemmob.Api.Analytics {
    /**<summary>Call 'CSFacebookAnalytis.Instance.Preload()' at the first application script to preload service.
     * <para> Current SDK: v8.1.0 https://developers.facebook.com/docs/unity/ </para>
     * </summary>*/
    public class GemFacebookAnalytics : Singleton<GemFacebookAnalytics> {
        private bool available;
        private event System.Action callOnAvailable;

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Gemmob/Api/Analytics/Facebook Settings")]
        public static void SelectSettings() {
            UnityEditor.Selection.activeObject = UnityEngine.Resources.Load<Facebook.Unity.Settings.FacebookSettings>("FacebookSettings");
        }
#endif

        protected override void Initialize() {
            GemFacebook.Instance.InitModule(InitFacebookAnalytics);
        }

        private void InitFacebookAnalytics() {
            if (available) return;
            available = true;
            if (callOnAvailable != null) callOnAvailable.Invoke();
            callOnAvailable = null;
        }
        //public void LogPurchase(float price, string currencyIsoCode = null, ParameterBuilder parameterBuilder) {

        //}

        //public void LogPurchase(float price, string currencyIsoCode = null, Dictionary<string, object> param = null) {
        //    FB.LogPurchase(price, currencyIsoCode, param);
        //}

        //public void LogPurchase(decimal price, string currencyIsoCode = null, Dictionary<string, object> param = null) {
        //    FB.LogPurchase(price, currencyIsoCode, param);
        //}

        public void LogEvent(string eventName, string paraName, object paraValue) {
            this.LogEvent(eventName, null, new Dictionary<string, object>() { { paraName, paraValue } });
        }

        public void LogEvent(string eventName, float value, string paraName, object paraValue) {
            this.LogEvent(eventName, value, new Dictionary<string, object>() { { paraName, paraValue } });
        }

        public void LogEvent(string eventName, ParameterBuilder parameterBuilder) {
            this.LogEvent(eventName, null, parameterBuilder.BuildDictObject());
        }

        public void LogEvent(string eventName, float value, ParameterBuilder parameterBuilder) {
            this.LogEvent(eventName, value, parameterBuilder.BuildDictObject());
        }

        public void LogEvent(string eventName, float? value = null, Dictionary<string, object> para = null) {
            if (available) {
                Logs.Log(string.Format("[FACEBOOK] [{0}] value={1}, paraCount={2}", eventName, value != null ? value.ToString() : "null", para != null ? para.Count : 0));
                if (UnityEngine.Application.isEditor) return;

                try { FB.LogAppEvent(eventName, value, para); }
                catch { throw; }
            }
            else {
                Logs.Log(string.Format("[FACEBOOK] Not available yet! Push to callback: [{0}] value={1}, paraCount={2}", eventName, value != null ? value.ToString() : "null", para != null ? para.Count : 0));
                callOnAvailable += () => {
                    FB.LogAppEvent(eventName, value, para);
                };
            }
        }

    }
}
#endif