using System;
using System.Collections;
using System.Collections.Generic;

#if FIREBASE
using Gemmob.Api.CSFirebases;
using Firebase;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Gemmob;

#endif
namespace Gemmob.Common {
    public class RemoteConfig : SingletonFreeAlive<RemoteConfig> {
        public bool isAvailable { get; private set; }
        public bool isInitialize { get; private set; }

        private readonly List<Action> waitForInitializeds = new List<Action>();

        private static readonly Dictionary<string, object> defaults = new Dictionary<string, object>();

        public void CallWhenInitialized(Action action) {
            waitForInitializeds.Add(action);
        }

        private void RunAfterInitialized() {
            foreach (var waitForInitialized in waitForInitializeds) {
                waitForInitialized.Invoke();
            }

            waitForInitializeds.Clear();
        }

        protected override void OnAwake() {
#if FIREBASE
            GemFirebase.Instance.InitModule(() => {
                if (GemFirebase.Instance.Available) {
                    var cacheExpiration = Config.IsDebug ? new TimeSpan(0, 0, 1, 0) : new TimeSpan(0);
                    //FirebaseRemoteConfig.SetDefaults(defaults);
                    FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);

                    FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheExpiration)
                        .ContinueWith(fetchTask => {
                            //if (FirebaseRemoteConfig.ActivateFetched()) {
                            //    Logs.Log("[REMOTE-CONFIG] Fetch Completed.");
                            //}
                            //else {
                            //    Logs.Log("[REMOTE-CONFIG] Fetch faild.");
                            //}
                            FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

                            isAvailable = true;
                            isInitialize = true;
                            RunAfterInitialized();
                        });
                    return;
                }

                isInitialize = true;
                RunAfterInitialized();
            });
#else
			RunAfterInitialized();
			isInitialize = true;
#endif
        }

        private string GetInternalDefaultString(string key) {
            object value;
            if (defaults.TryGetValue(key, out value)) {
                return value.ToString();
            }

            return String.Empty;
        }

        private bool GetInternalDefaultBool(string key) {
            object value;
            if (defaults.TryGetValue(key, out value)) {
                if (value is bool) {
                    return (bool)value;
                }
            }

            return false;
        }

        private long GetInternalDefaultLong(string key) {
            object value;
            if (defaults.TryGetValue(key, out value)) {
                if (value is long) {
                    return (long)value;
                }

                if (value is int) {
                    return (int)value;
                }
            }

            return 0;
        }

        private double GetInternalDefaultDouble(string key) {
            object value;
            if (defaults.TryGetValue(key, out value)) {
                if (value is float) {
                    return (float)value;
                }

                if (value is double) {
                    return (double)value;
                }
            }

            return 0;
        }

        public static string GetString(string key, string configNamespace = null) {
#if FIREBASE
            if (Instance.isAvailable) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.StringValue;
            }
#endif
            var value = Instance.GetInternalDefaultString(key);
            Logs.Log($"[REMOTE-CONFIG] Get default key={key}, value=[{value}]");
            return value;
        }

        public static bool GetBool(string key, string configNamespace = null) {
#if FIREBASE
            if (Instance.isAvailable) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.BooleanValue;
            }
#endif
            var value = Instance.GetInternalDefaultBool(key);
            Logs.Log($"[REMOTE-CONFIG] Get default key={key}, value=[{value}]");
            return value;
        }
#if FIREBASE
        private static ConfigValue GetConfigValue(string key, string configNamespace = null) {
            var configValue = FirebaseRemoteConfig.DefaultInstance.GetValue(key);
            
            if (string.IsNullOrEmpty(configNamespace)) {
                Logs.Log($"[REMOTE-CONFIG] Get key={key}, value=[{configValue.StringValue}]");
            }
            else {
                Logs.Log($"[REMOTE-CONFIG] Get key={key}, namespace={configNamespace}, value=[{configValue.StringValue}]");
            }

            return configValue;
        }
#endif
        public static long GetLong(string key, string configNamespace = null) {
#if FIREBASE
            if (Instance.isAvailable) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.LongValue;
            }
#endif
            var value = Instance.GetInternalDefaultLong(key);
            Logs.Log($"[REMOTE-CONFIG] Get default key={key}, value=[{value}]");
            return value;
        }

        public static double GetDouble(string key, string configNamespace = null) {
#if FIREBASE
            if (Instance.isAvailable) {
                var configValue = GetConfigValue(key, configNamespace);
                return configValue.DoubleValue;
            }
#endif
            var value = Instance.GetInternalDefaultDouble(key);
            Logs.Log($"[REMOTE-CONFIG] Get default key={key}, value=[{value}]");
            return value;
        }


        public static Dictionary<string, object> GetDefaults() {
            return defaults;
        }

        public static void AddDefault(string key, object values) {
            Logs.Log($"[REMOTE-CONFIG] Add Default {key}={values}");
            defaults.Add(key, values);
        }

        public static void SetDefault() {
#if FIREBASE

            if (Instance.isInitialize) {
                if (Instance.isAvailable) {
                    Logs.Log("[REMOTE-CONFIG] Set Default");
                    FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
                }
            }

#endif
        }

        private void WaitForFetchKey(string key, Action task) {
            Logs.Log($"[REMOTE-CONFIG] Wait for fetch key  : {key}");
            CallWhenInitialized(() => { task.Invoke(); });
        }


        public static void GetStringAsync(string key, Action<string> action, string configNamespace = null) {
            if (Instance.isInitialize) {
                action.Invoke(GetString(key, configNamespace));
            }
            else {
                Instance.WaitForFetchKey(key, () => { action.Invoke(GetString(key, configNamespace)); });
            }
        }
        public void ReloadGetStringAsyncs(string key, Action<string> action) {
#if FIREBASE
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(new TimeSpan(0))
                        .ContinueWith(fetchTask => {
                            //if (FirebaseRemoteConfig.ActivateFetched()) {
                            //    Logs.LogError("[REMOTE-CONFIG] Fetch Completed.");
                            //}
                            //else {
                            //    Logs.LogError("[REMOTE-CONFIG] Fetch faild.");
                            //}
                            FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

                            CallWhenInitialized(() => { action.Invoke(GetString(key)); });
                            RunAfterInitialized();
                        });
#endif
        }
        public static void GetLongAsync(string key, Action<long> action, string configNamespace = null) {
            if (Instance.isInitialize) {
                action.Invoke(GetLong(key, configNamespace));
            }
            else {
                Instance.WaitForFetchKey(key, () => { action.Invoke(GetLong(key, configNamespace)); });
            }
        }

        public static void GetBoolAsync(string key, Action<bool> action, string configNamespace = null) {
            if (Instance.isInitialize) {
                action.Invoke(GetBool(key, configNamespace));
            }
            else {
                Instance.WaitForFetchKey(key, () => { action.Invoke(GetBool(key, configNamespace)); });
            }
        }

        public static void GetBoolAsync(string key, Action<double> action, string configNamespace = null) {
            if (Instance.isInitialize) {
                action.Invoke(GetDouble(key, configNamespace));
            }
            else {
                Instance.WaitForFetchKey(key, () => { action.Invoke(GetDouble(key, configNamespace)); });
            }
        }
    }
}