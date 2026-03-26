
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BossCombatPanelAction", menuName = "Resource/GameAction/CombatPanel/BossCombatPanelAction")]
public class BossCombatPanelAction : CombatPanelAction {
    public override void Execute(object user, Action onCompleted) {

    }
    public override CombatPanel GetCombat() {
        return IngameHUD.Instance.Show<BossModeCombatPanel>();
    }
}