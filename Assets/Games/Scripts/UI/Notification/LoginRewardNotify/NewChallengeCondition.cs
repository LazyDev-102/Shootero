using UnityEngine;

[CreateAssetMenu(fileName = "NewChallengeCondition", menuName = "Resource/Conditions/Challenge/NewChallengeCondition")]
public class NewChallengeCondition : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.Challenge.IsFirstOpen()
            || GameResources.Instance.Challenge.CanShowNotification();
    }
}