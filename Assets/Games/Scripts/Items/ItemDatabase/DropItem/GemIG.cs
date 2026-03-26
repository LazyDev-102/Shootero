using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GemIG", menuName = "Resource/Item/Currency/GemIG")]
public class GemIG : Item {
    public override void Claim(int amount) {
        GameManager.Instance.AddClaimedItem(ConstantItemID.GemId, amount);
        GameResources.Instance.Inventory.Add(ConstantItemID.GemId, amount);
    }
}