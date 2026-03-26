
using UnityEngine;
using System.Collections.Generic;

namespace Gemmob.Api.Analytics {
    [CreateAssetMenu(fileName = "AnalyticsSettings", menuName = "Gemmob/Api/AnalyticsSettings")]
    internal class AnalyticsSettings : ScriptableObject {
        public bool useFirebase;
        public bool useFacebook;
        public bool useAppsFlyer;
        public bool useAdjust;
        public bool useGA;
        public bool useKochava;

        public const string FirebaseDefine = "FIREBASE";
        public const string FacebookDefine = "FACEBOOK";
        public const string AppsFlyerDefine = "APPSFLYER";
        public const string AdjustDefine = "ADJUST";
        public const string GADefine = "GA";
        public const string KochavaDefine = "KOCHAVA";
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(AnalyticsSettings))]
    internal class AnalyticsSettingsEditor : UnityEditor.Editor {

        [UnityEditor.MenuItem("Gemmob/Api/AnalyticsSettings")]
        public static void OpenSettingsFile() {
            UnityEditor.Selection.activeObject = UnityEditor.AssetDatabase.LoadAssetAtPath<AnalyticsSettings>("Assets/Gemmob/Api/Analytics/Editor/AnalyticsSettings.asset");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUILayout.Space(20);
            if (GUILayout.Button("Save")) {
                SaveSetting();
            }
        }

        private void SaveSetting() {
            var setting = target as AnalyticsSettings;

            Gemmob.EditorTools.ScriptingDefineHelper.UpdateSymbols(
                new KeyValuePair<string, bool>(AnalyticsSettings.FirebaseDefine, setting.useFirebase),
                new KeyValuePair<string, bool>(AnalyticsSettings.FacebookDefine, setting.useFacebook),
                new KeyValuePair<string, bool>(AnalyticsSettings.AppsFlyerDefine, setting.useAppsFlyer),
                new KeyValuePair<string, bool>(AnalyticsSettings.AdjustDefine, setting.useAdjust),
                new KeyValuePair<string, bool>(AnalyticsSettings.GADefine, setting.useGA),
                new KeyValuePair<string, bool>(AnalyticsSettings.KochavaDefine, setting.useKochava)
            );

            UnityEditor.EditorUtility.SetDirty(UnityEditor.Selection.activeObject);
            UnityEditor.AssetDatabase.SaveAssets();
        }

    }
#endif
}
