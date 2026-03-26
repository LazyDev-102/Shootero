
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XmasModePreload", menuName = "Resource/HardData/Preload/XmasModePreload")]
public class XmasModePreload : GameAction {
    public override void Execute(object user, Action onCompleted) {
        GameResources.Instance.Xmas.Preload();
    }
}
