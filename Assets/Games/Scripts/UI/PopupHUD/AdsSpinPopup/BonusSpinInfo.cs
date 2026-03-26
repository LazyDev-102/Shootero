using Helper;
using UnityEngine;

[System.Serializable]
public class BonusSpinInfo {
    [SerializeField] private float chipAfkPoint;
    [SerializeField] private ItemClaim reward;
    [SerializeField] private bool isSpecial;

    public Sprite Icon { get => reward.Icon; }
    public string Name { get => reward.Name; }
    public int Amount { get => reward.Amount; }
    public ItemClaim Reward { get => reward; }
    public bool IsSpecial { get => isSpecial; }

    public void Assign() {
        if (reward == null)
            return;
        if (reward.Id == ConstantItemID.ChipIG) {
            var value = (GameResources.Instance.ChipPerSecond * Constant.HourToSecond * chipAfkPoint).ConvertToInt();
            if (value < 1)
                value = 1;
            reward.Amount = value;
        }
    }
    public void Claim() {
        reward.Claim();
    }
}