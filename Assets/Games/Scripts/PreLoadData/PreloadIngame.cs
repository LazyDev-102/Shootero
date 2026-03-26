using UnityEngine;

[CreateAssetMenu(fileName = "PreloadIngame", menuName = "Resource/HardData/Preload/PreloadIngame")]
public class PreloadIngame : ScriptableObject {
    [SerializeField] GameAction[] modes;
    public void Preload(int modeIndex) {
        ShipPreload();
        modes[modeIndex].Execute();
    }
    private void ShipPreload() {
        ShipBase shipBase = GameResources.Instance.Ship.GetCurrentShip().ShipPrefab;
        if (shipBase) {
            shipBase.PreloadIngame();
        }
    }
}
