using System;
using UnityEngine;
using GameSystem.Common.UI;

[CreateAssetMenu(fileName = "WaveMysteryStationAction", menuName = "Resource/GameAction/StartEndWave/WaveMysteryStationAction")]
public class WaveMysteryStationAction : GameAction {
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
            ship.ShipMove.LockTouch(false);
            HUDManager.IgnoreUserInput(false);
            onCompleted?.Invoke();
        };
        var mystery = IngameHUD.Instance.MysteryStation;
        if (mystery) {
            mystery.Show();
            mystery.AddOnClose(newAction);
        }
        else {
            newAction?.Invoke();
        }
    }
}