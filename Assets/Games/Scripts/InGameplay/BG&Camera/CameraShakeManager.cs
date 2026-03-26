using UnityEngine;
using DG.Tweening;
using Gemmob;

public class CameraShakeManager : SingletonBind<CameraShakeManager> {
    [SerializeField] private Transform currentCamera;
    [SerializeField] private CameraShakeTypeInfor[] typeInfors;
    [SerializeField] private CameraFollowShip cameraFollowShip;
    private Tween currentPositionTween;

    private int currentPriovity;


    public void ShakeCamera(CameraShakeType type) {
        CameraShakeTypeInfor infor = typeInfors[(int)type];
        if (infor.use) {
            ShakeCameraPosition(infor.duration, infor.strength, infor.vibrato, infor.randommess, infor.snapping, infor.fadeout, infor.ease, (int)type);
        }

    }

    public void ShakeCameraPosition(float duration, float strenght, int vibrato, float randommess, bool snapping, bool fadeout, Ease ease, int priovity) {
        if (priovity > 0 && (currentPriovity > priovity)) {
            return;
        }
        FollowAction(false);
        currentPriovity = priovity;
        currentPositionTween.Kill(true);
        currentPositionTween = currentCamera.DOShakePosition(duration, strenght, vibrato, randommess, snapping, fadeout).SetEase(ease).OnComplete(() => {
            currentPriovity = -1;
            FollowAction(true);
        });
    }
    private void FollowAction(bool status) {
        if (cameraFollowShip != null) {
            cameraFollowShip.SetCanFollowStatus(status);
        }
    }
#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ShakeCamera(CameraShakeType.Weak);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ShakeCamera(CameraShakeType.Normal);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ShakeCamera(CameraShakeType.Strong);

        }
    }
#endif
}

public enum CameraShakeType {
    None = 0, Weak = 1, Normal = 2, Strong = 3
}

[System.Serializable]
public class CameraShakeTypeInfor {
    public CameraShakeType type;
    public float duration;
    public float strength = 1f;
    public int vibrato = 10;
    public float randommess = 90;
    public bool snapping = false;
    public bool fadeout = true;
    public Ease ease;
    public bool use;
}
