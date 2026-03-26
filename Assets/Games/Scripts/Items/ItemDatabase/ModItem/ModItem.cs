using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Resource/Item/Mod/ModItem")]
public class ModItem : Item {
    [SerializeField] private ModData mod;
    public override void Claim(int amount) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship && mod != null) {
            mod.ApplyTo(ship);
            IngameHUD.Instance.Combat.ShowModInfo.AddModInfor(mod);
        }
    }
}