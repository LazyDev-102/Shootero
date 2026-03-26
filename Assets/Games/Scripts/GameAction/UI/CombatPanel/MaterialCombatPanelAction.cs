
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/MaterialCombatPanelAction")]
public class MaterialCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<MaterialModeCombatPanel>();
    }
}