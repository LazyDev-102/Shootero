
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XmasCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/XmasCombatPanelAction")]
public class XmasCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<XmasCombatPanel>();
    }
}