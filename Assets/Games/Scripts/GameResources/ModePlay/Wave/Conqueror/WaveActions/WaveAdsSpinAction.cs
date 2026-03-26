using System;
using UnityEngine;
using GameSystem.Common.UI;

[CreateAssetMenu(fileName = "WaveAdsSpinAction", menuName = "Resource/GameAction/StartEndWave/WaveAdsSpinAction")]
public class WaveAdsSpinAction : GameAction {
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
        var adsSpin = IngameHUD.Instance.AdsSpin;
        if (adsSpin) {
            adsSpin.Show();
            adsSpin.AddOnClose(newAction);
        }
        else {
            newAction?.Invoke();
        }
    }
}