using UnityEngine;

[CreateAssetMenu(fileName = "PreloadGame", menuName = "Resource/HardData/Preload/PreloadGame")]
public class PreloadGame : ScriptableObject {
    [SerializeField] private PreloadIngame preloadIngame;
    [SerializeField] private PreloadOpenApp preloadOpenApp;


    public void PreloadOpenApp() {
        if (preloadOpenApp) {
            preloadOpenApp.Preload();
        }
    }

    public void PreloadIngame() {
        if (preloadIngame) {
            preloadIngame.Preload((int)IngameData.currentGameMode);
        }
    }
}
