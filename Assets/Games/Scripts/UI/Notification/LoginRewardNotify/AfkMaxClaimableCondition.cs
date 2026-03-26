using UnityEngine;

[CreateAssetMenu(fileName = "AfkMaxClaimableCondition", menuName = "Resource/Conditions/Afk/AfkMaxClaimableCondition")]
public class AfkMaxClaimableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.AFK.Maxable;
    }
}