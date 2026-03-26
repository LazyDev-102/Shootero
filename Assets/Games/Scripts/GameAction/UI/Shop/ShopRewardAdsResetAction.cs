
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopRewardAdsResetAction", menuName = "Resource/GameAction/Shop/RewardAdsResetAction")]
public class ShopRewardAdsResetAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.ShopData.RewardAds.CheckResetData();
    }
}
