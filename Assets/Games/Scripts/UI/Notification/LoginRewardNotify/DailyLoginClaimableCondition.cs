using UnityEngine;

[CreateAssetMenu(fileName = "DailyLoginClaimableCondition", menuName = "Resource/Conditions/DailyLogin/DailyLoginClaimableCondition")]
public class DailyLoginClaimableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.DailyLoginData.Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
    }
}