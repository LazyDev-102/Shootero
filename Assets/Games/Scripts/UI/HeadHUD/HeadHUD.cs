using GameSystem.Common.UI;
using Helper;
using UnityEngine;

public class HeadHUD : HUD<HeadHUD> {

    protected override void Awake() {
        base.Awake();
        GetComponentInParent<Canvas>().worldCamera = CameraHelper.Camera;
    }
    protected override void Start() {
        base.Start();

    }

    protected override void OnDestroy() {
        base.OnDestroy();
    }

    public override bool OnUpdate() {
        return false;
    }

}
