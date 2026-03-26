using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RaidBossModePreload", menuName = "Resource/HardData/Preload/RaidBossModePreload")]
public class RaidBossModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        //GameResourcesIG.Instance.RaidBossModeData.Preload();
    }
}
