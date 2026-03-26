using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveFullHealCondition", menuName = "Resource/Conditions/StartEndWave/WaveFullHealCondition")]
public class WaveFullHealCondition : WaveCondition<ShipBase> {
    public override bool Action(ShipBase target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(ShipBase ship) {
#if UNITY_EDITOR
        return EMAdManager.Instance.HasRewardAds() && GameResources.Instance.FullHeal.CanAppear(GameManager.Instance.GameLoader.Ship);
#else
        return Gemmob.Networker.IsInternetAvaiable && EMAdManager.Instance.HasRewardAds() && GameResources.Instance.FullHeal.CanAppear(GameManager.Instance.GameLoader.Ship);
#endif
    }

    public override bool CheckCondition(object target) {
#if UNITY_EDITOR
        return EMAdManager.Instance.HasRewardAds() && GameResources.Instance.FullHeal.CanAppear(GameManager.Instance.GameLoader.Ship);
#else
        return Gemmob.Networker.IsInternetAvaiable && EMAdManager.Instance.HasRewardAds() && GameResources.Instance.FullHeal.CanAppear(GameManager.Instance.GameLoader.Ship);
#endif
    }
}
