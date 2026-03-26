using DG.Tweening;
using System;
using UnityEngine;

public class DotweenActive : DotweenTransition {
    [SerializeField] private Transform target;
    [SerializeField] private bool isShow;

    private void Reset() {
        target = transform;
    }

    public override void ResetState() {
        if (target != null)
            target.gameObject.SetActive(!isShow);
        if (Tween != null)
            Tween.Kill();
    }

    public override void DoTransition(Action onCompleted, bool restart) {
        if (restart) {
            ResetState();
        }

        Tween = DOVirtual.DelayedCall(Duration, () => {
            target.gameObject.SetActive(isShow);
        });

        Tween.SetUpdate(IgnoreTimeScale)
                             .SetDelay(Delay)
                             .OnComplete(() => onCompleted?.Invoke());


    }
}
