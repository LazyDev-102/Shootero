using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class RotateObject : MonoBehaviour {
    [SerializeField] float duration;
    Vector3 des;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotateTweener;

    private void Awake() {
        des = Vector3.forward * 180 * Helper.RandomHelper.RandomChoose(1, -1);
    }
    private void OnEnable() {
        if (rotateTweener != null)
            rotateTweener.Kill();
        rotateTweener = transform.DOLocalRotate(des, duration).SetLoops(-1, LoopType.Incremental);
    }
}
