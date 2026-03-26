

using DG.Tweening;
using System;
using UnityEngine;

public class DotweenMoveDirection : DotweenTransition {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 direction;

    private void Reset() {
        target = transform;
    }

    public override void ResetState() {
        if (Tween != null)
            Tween.Kill();
    }

    public override void DoTransition(Action onCompleted, bool restart) {
        if (restart) {
            ResetState();
        }
        Tween = target.DOMove(target.position + direction, Duration)
              .SetSpeedBased(SpeedBase)
                   .SetEase(Cuver)
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

