
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InfinityCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/InfinityCombatPanelAction")]
public class InfinityCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<InfinityCombatPanel>();
    }
}