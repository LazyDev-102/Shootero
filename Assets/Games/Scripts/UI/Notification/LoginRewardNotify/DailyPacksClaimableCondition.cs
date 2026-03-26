using UnityEngine;

[CreateAssetMenu(fileName = "DailyPacksClaimableCondition", menuName = "Resource/Conditions/DailyPacks/DailyPacksClaimableCondition")]
public class DailyPacksClaimableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        var data = GameResources.Instance.DailyPacksData;
        foreach (var item in data.GetFreePack()) {
            if (item != null && item.Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year)) {
                return true;
            }
        }
        return false;
    }
}
