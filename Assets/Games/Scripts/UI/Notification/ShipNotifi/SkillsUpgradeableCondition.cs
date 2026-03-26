using UnityEngine;

[CreateAssetMenu(fileName = "SkillsUpgradeableCondition", menuName = "Resource/Conditions/Skills/SkillsUpgradeableCondition")]
public class SkillsUpgradeableCondition : GameCondition<ShipInfor> {

    public override bool CheckCondition(ShipInfor target) {
        return GameResources.Instance.SkillSystemData.UpgradeableAnySkill();
    }
}
