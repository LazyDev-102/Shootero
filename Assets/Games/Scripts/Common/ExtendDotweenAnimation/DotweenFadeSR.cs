

using DG.Tweening;
using Helper;
using System;
using UnityEngine;

public class DotweenFadeSR : DotweenTransition {
    [SerializeField] private SpriteRenderer target;
    [SerializeField] private float from;
    [SerializeField] private float to;

    private void Reset() {
        target = GetComponent<SpriteRenderer>();
    }

    public override void ResetState() {
        if (target != null)
            target.ChangeAlpha(from);
        if (Tween != null)
            Tween.Kill();
    }

    public override void DoTransition(Action onCompleted, bool restart) {
        if (restart) {
            ResetState();
        }
        Tween = target.DOFade(to, Duration)
                      .SetSpeedBased(SpeedBase)
                    .SetEase(Ease)
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
