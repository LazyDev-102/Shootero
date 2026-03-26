using UnityEngine;

[CreateAssetMenu(fileName = "MissionClaimableCondition", menuName = "Resource/Conditions/Mission/MissionClaimableCondition")]
public class MissionClaimableCondition : GameCondition {
    [SerializeField] private NewChallengeCondition newChallengeCondition;
    [SerializeField] private ChallengeClaimableCondition challengeClaimableCondition;
    public override bool CheckCondition(object target) {
        return GameResources.Instance.DailyMission.Claimable()
            || newChallengeCondition.CheckCondition(null)
            || challengeClaimableCondition.CheckCondition(null);
    }
}