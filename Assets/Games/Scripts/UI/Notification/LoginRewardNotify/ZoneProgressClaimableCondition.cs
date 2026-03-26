using UnityEngine;

[CreateAssetMenu(fileName = "ZoneProgressClaimableCondition", menuName = "Resource/Conditions/ZoneProgress/ZoneProgressClaimableCondition")]
public class ZoneProgressClaimableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        ConquerorData data = GameResources.Instance.ConquerorData;
        (int cRewardWave, int cRewardZone) = GameResources.Instance.LevelProgress.Datas.GetCurrentLevelClaimable();
        var uZone = data.UnlockZone + 1;
        var uWave = data.ZoneDatas[data.UnlockZone].HighestWave;
        return cRewardZone < uZone || cRewardZone == uZone && cRewardWave <= uWave;
    }
}