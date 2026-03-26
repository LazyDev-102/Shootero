using UnityEngine;

[CreateAssetMenu(fileName = "AbilityUpgradeableCondition", menuName = "Resource/Conditions/Ability/AbilityUpgradeableCondition")]
public class AbilityUpgradeableCondition : GameCondition {
    public override bool CheckCondition(object target) {
        var levelProgress = GameResources.Instance.LevelProgress;
        return levelProgress.Datas.UnlockFeatures.CanUnlockAbility(levelProgress.GetCurrentLevel() + 1) && GameResourceLoader.Initialized && GameResources.Instance.AbilityCollectorData.CanUpgrade;
    }
}