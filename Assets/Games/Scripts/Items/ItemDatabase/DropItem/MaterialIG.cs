using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialIG", menuName = "Resource/Item/Currency/MaterialIG")]
public class MaterialIG : Item {
    [SerializeField, Range(0f, 1f)] private float percentHeal;
    public override void Claim(int amount) {
        GameManager.Instance.AddClaimedItem(ConstantItemID.RandomMatId, amount);
    }
}