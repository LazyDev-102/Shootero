
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GearCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/GearCombatPanelAction")]
public class GearCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<GearModeCombatPanel>();
    }
}