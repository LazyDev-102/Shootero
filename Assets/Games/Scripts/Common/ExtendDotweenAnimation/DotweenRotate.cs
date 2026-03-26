
using DG.Tweening;
using System;
using UnityEngine;

public class DotweenRotate : DotweenTransition {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 from;
    [SerializeField] private Vector3 to;

    private void Reset() {
        target = transform;
    }

    public override void ResetState() {
        if (target != null)
            target.localScale = from;
        if (Tween != null)
            Tween.Kill();
    }

    public override void DoTransition(Action onCompleted, bool restart) {
        if (restart) {
            ResetState();
        }
        Tween = target.DOLocalRotate(to, Duration)
                             .SetSpeedBased(SpeedBase)
                             .SetUpdate(IgnoreTimeScale)
                             .SetDelay(Delay)
                             .OnComplete(() => onCompleted?.Invoke());
        if (UseCuver) {
            Tween.SetEase(Cuver);
        }
        else {
            Tween.SetEase(Ease);
        }
        if (Loops != 1) {
            Tween.SetLoops(Loops, LoopType);
        }
    }
}
