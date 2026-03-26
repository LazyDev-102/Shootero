using UnityEngine;

[CreateAssetMenu(fileName = "ChipIG", menuName = "Resource/Item/Currency/ChipIG")]
public class ChipIG : Item {
    public override void Claim(int amount) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship) {
            ship.AddChip(amount);
            GameManager.Instance.AddClaimedItem(ConstantItemID.ChipId, amount);
            GameResources.Instance.Inventory.Add(ConstantItemID.ChipId, amount);
        }
    }
}