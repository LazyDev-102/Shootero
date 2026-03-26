using UnityEngine;

[CreateAssetMenu(fileName = "RookieLoginClaimableCondition", menuName = "Resource/Conditions/Rookie/RookieLoginClaimableCondition")]
public class RookieLoginClaimableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.RookieLoginData.Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
    }
}