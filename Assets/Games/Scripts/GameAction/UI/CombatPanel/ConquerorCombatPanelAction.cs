using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConquerorCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/ConquerorCombatPanelAction")]
public class ConquerorCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<ConquerorCombatPanel>();
    }
}
