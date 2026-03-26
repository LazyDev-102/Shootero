using UnityEngine;

[CreateAssetMenu(fileName = "XPItem", menuName = "Resource/Item/Currency/XPItem")]
public class XPItem : Item {
    public override void Claim(int amount) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship != null)
            ship.ShipLevel.AddExp(amount);
    }
}