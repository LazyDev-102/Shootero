using System;
using UnityEngine;
using GameSystem.Common.UI;

[CreateAssetMenu(fileName = "WaveFullHealAction", menuName = "Resource/GameAction/StartEndWave/WaveFullHealAction")]
public class WaveFullHealAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship == null) {
            onCompleted?.Invoke();
            return;
        }
        ship.ShipAttack.ChangeStateShot(false);
        ship.ShipMove.LockTouch(true);
        Action newAction = () => {
            ship.ShipAttack.ChangeStateShot(true);
            HUDManager.IgnoreUserInput(false);
            ship.ShipMove.LockTouch(false);
            onCompleted?.Invoke();
        };
        var fullHeal = IngameHUD.Instance.FullHeal;
        if (fullHeal) {
            fullHeal.Show();
            fullHeal.AddOnClose(newAction);
        }
        else {
            newAction?.Invoke();
        }
    }
}
