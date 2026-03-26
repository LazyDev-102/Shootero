using UnityEngine;

[CreateAssetMenu(fileName = "ChallengeClaimableCondition", menuName = "Resource/Conditions/Challenge/ChallengeClaimableCondition")]
public class ChallengeClaimableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.Challenge.Claimable();
    }
}