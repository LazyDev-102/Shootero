
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenModePreload", menuName = "Resource/HardData/Preload/HalloweenModePreload")]
public class HalloweenModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.Halloween.Preload();
    }
}
