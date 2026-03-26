using UnityEngine;

[CreateAssetMenu(fileName = "Bootstrap", menuName = "Resource/Bootstrap")]
public class Bootstrap : ScriptableObject {
    [SerializeField] private bool useTargetFPS = true;
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private bool logEnable = true;
    [Header("API Preload")]
    [SerializeField] private bool preloadAnalytics = true;
    [SerializeField] private bool preloadAds = true;
    [SerializeField] private bool preloadIAP = true;


    private void OnEnable() {
        if (useTargetFPS) {
            Application.targetFrameRate = targetFPS;
        }

#if !CHEAT
        Gemmob.Logs.Settings.LogEnable = logEnable;
#else
        Gemmob.Logs.Settings.LogEnable = true;
#endif
    }

    public void AssignStart() {
        if (preloadAnalytics) {
            Tracking.Instance.Preload();
        }

        //if (preloadAds) {
        //    EasyMobile.RuntimeManager.Init();
        //}

        if (preloadIAP) {
            GameIAP.Instance.Preload();
        }
        GameResources.Instance.PreloadGame.PreloadOpenApp();
    }
}
