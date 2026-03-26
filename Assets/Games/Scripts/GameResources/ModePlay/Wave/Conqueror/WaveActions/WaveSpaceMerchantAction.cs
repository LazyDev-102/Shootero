using System;
using UnityEngine;
using GameSystem.Common.UI;

[CreateAssetMenu(fileName = "WaveSpaceMerchantAction", menuName = "Resource/GameAction/StartEndWave/WaveSpaceMerchantAction")]
public class WaveSpaceMerchantAction : GameAction {
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
        var spaceMerchant = IngameHUD.Instance.SpaceMerchant;
        if (spaceMerchant) {
            spaceMerchant.Show();
            spaceMerchant.AddOnClose(newAction);
        }
        else {
            newAction?.Invoke();
        }
    }
}