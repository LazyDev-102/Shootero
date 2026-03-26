using DG.Tweening;
using System;
using UnityEngine;

public class DotweenShake : DotweenTransition {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 strength;
    [SerializeField] private ShakeType type;
    [SerializeField] private int vibrato;
    [SerializeField] private float randommess;
    [SerializeField] private bool fadeOut;


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

        switch (type) {
            case ShakeType.Position: {
                Tween = target.DOShakePosition(Duration, strength, vibrato, randommess, false, fadeOut);
                break;
            }
            case ShakeType.Rotation: {
                Tween = target.DOShakeRotation(Duration, strength, vibrato, randommess, fadeOut);
                break;
            }
            case ShakeType.Scale: {
                Tween = target.DOShakeScale(Duration, strength, vibrato, randommess, fadeOut);
                break;
            }
        }
        Tween.SetSpeedBased(SpeedBase)
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

    public enum ShakeType {
        Position, Rotation, Scale
    }
}
