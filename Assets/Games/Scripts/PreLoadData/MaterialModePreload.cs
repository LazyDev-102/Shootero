using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialModePreload", menuName = "Resource/HardData/Preload/MaterialModePreload")]
public class MaterialModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.MaterialModeData.Preload();
    }
}
