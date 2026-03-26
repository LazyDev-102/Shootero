using UnityEngine;
using System;
using Helper;

[Serializable]
public class AdsSpinInfo {
    [SerializeField] private float chipAfkPoint;
    [SerializeField] private ItemClaim[] reward;
    [SerializeField] private bool isMod;
    public Sprite Icon { get => reward[0].Icon; }
    public string Name { get => reward[0].Name; }
    public int Amount { get => reward[0].Amount; }
    public ItemClaim[] Reward { get => IsMod ? new ItemClaim[] { reward[0] } : reward; }
    public bool IsMod { get => isMod; }

    public void Assign() {
        foreach (var item in reward) {
            if (item == null)
                continue;
            if (item.Id == ConstantItemID.ChipIG) {
                var value = (GameResources.Instance.ChipPerSecond * Constant.HourToSecond * chipAfkPoint).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
            }
            if (item.Id == ConstantItemID.RandomMatId) {
                var value = (GameResources.Instance.MaterialPerSecond * Constant.HourToSecond * chipAfkPoint).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
            }
        }
    }
    public void GenMod() {
        if (!isMod)
            return;
        int loopTimes = 0;
        do {
            reward[0] = reward[UnityEngine.Random.Range(1, reward.Length)];
            loopTimes++;
            if (loopTimes > 5)
                break;
        } while (!GameResources.Instance.AdsSpin.OneTimeModLoadable && reward[0].Id == GameResources.Instance.AdsSpin.OneTimeMod.Id);
        if (reward[0].Id == GameResources.Instance.AdsSpin.OneTimeMod.Id)
            GameResources.Instance.AdsSpin.OneTimeModLoadable = false;
    }
    public void Claim() {
        foreach (var item in reward) {
            item.Claim();
        }
    }
}