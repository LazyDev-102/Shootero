using UnityEngine;


[CreateAssetMenu(fileName = "NewGearCondition", menuName = "Resource/Conditions/Gear/New Gear Condition")]
public class NewGearCondition : GameCondition<GearSoftData> {
    public override bool CheckCondition(GearSoftData target) {
        return !target.IsNewChecked;
    }
}
