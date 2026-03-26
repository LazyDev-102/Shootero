using UnityEngine;

[CreateAssetMenu(fileName = "ExpItem", menuName = "Resource/Item/Currency/ExpItem")]
public class EXPItem : Item {
    public override void Claim(int amount) {
        GameResources.Instance.LevelProgress.AddExp(amount);
    }
}