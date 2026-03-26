using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GearModePreload", menuName = "Resource/HardData/Preload/GearModePreload")]
public class GearModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.GearModeData.Preload();
    }
}
