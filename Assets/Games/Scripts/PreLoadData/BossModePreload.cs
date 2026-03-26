using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BossModePreload", menuName = "Resource/HardData/Preload/BossModePreload")]
public class BossModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.BossModeData.Preload();
    }
}
