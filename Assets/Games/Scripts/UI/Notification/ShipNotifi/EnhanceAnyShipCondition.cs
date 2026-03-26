using UnityEngine;

[CreateAssetMenu(fileName = "EnhanceAnyShipCondition", menuName = "Resource/Conditions/Ship/EnhanceAnyShipCondition")]
public class EnhanceAnyShipCondition : GameCondition {
    [SerializeField] private EnhanceShipCondition enhanceShipCondition;
    public override bool CheckCondition(object target) {
        foreach (var item in GameResources.Instance.Ship.Datas) {
            if (enhanceShipCondition.CheckCondition(item))
                return true;
        }
        return false;
    }
}