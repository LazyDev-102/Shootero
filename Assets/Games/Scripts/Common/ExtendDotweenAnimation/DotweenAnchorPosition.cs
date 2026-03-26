using DG.Tweening;
using System;
using UnityEngine;

public class DotweenAnchorPosition : DotweenTransition {
    [SerializeField] private RectTransform target;
    [SerializeField] private Vector2 from;
    [SerializeField] private Vector2 to;
    [SerializeField] private bool snapping = false;

    private void Reset() {
        target = transform as RectTransform;
    }

    public override void ResetState() {
        if (target != null)
            target.anchoredPosition = from;
        if (Tween != null)
            Tween.Kill();
    }

    public override void DoTransition(Action onCompleted, bool restart) {
        if (restart) {
            ResetState();
        }

        Tween = target.DOAnchorPos(to, Duration)
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
