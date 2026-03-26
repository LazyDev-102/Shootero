using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PreloadOpenApp", menuName = "Resource/HardData/Preload/PreloadOpenApp")]
public class PreloadOpenApp : ScriptableObject {
    [Header("Text Ingame")]
    [SerializeField] private TextIngame[] textIngames;

    public void Preload() {
        GameResources.Instance.Drop.PreloadOpenApp();
        foreach (var t in textIngames) {
            t.PreloadOpenApp();
        }

        int curLevel = GameResources.Instance.LevelProgress.GetCurrentLevel();
        int levelExt = curLevel + curLevel < 10 ? 10 : 5;
        List<ModData> mods = GameResources.Instance.ModGenerator.GetAllModUnlocked(levelExt);
        foreach (var m in mods) {
            m.PreloadOpenApp();
        }
    }
}
