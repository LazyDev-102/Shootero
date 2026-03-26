
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/HalloweenCombatPanelAction")]
public class HalloweenCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<HalloweenCombatPanel>();
    }
}