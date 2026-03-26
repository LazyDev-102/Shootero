using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InfinityModePreload", menuName = "Resource/HardData/Preload/InfinityModePreload")]
public class InfinityModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.InfinityModeData.Preload();
    }
}
