using DG.Tweening;
using System;
using UnityEngine;

public class DotweenPunch : DotweenTransition {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 punch;
    [SerializeField] private PunchType type;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private float elasticity = 1;

    private void Reset() {
        target = transform;
    }

    public override void ResetState() {
        if (target != null) {
            switch (type) {
                case PunchType.Position: {
                    target.transform.localPosition = start;
                    break;
                }
                case PunchType.Rotation: {
                    target.transform.localEulerAngles = start;

                    break;
                }
                case PunchType.Scale: {
                    target.transform.localScale = start;
                    break;
                }
            }
        }
        if (Tween != null)
            Tween.Kill();
    }

    public override void DoTransition(Action onCompleted, bool restart) {
        if (restart) {
            ResetState();
        }

        switch (type) {
            case PunchType.Position: {
                Tween = target.DOPunchPosition(punch, Duration, vibrato, elasticity);
                break;
            }
            case PunchType.Rotation: {
                Tween = target.DOPunchRotation(punch, Duration, vibrato, elasticity);
                break;
            }
            case PunchType.Scale: {
                Tween = target.DOPunchScale(punch, Duration, vibrato, elasticity);
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

    public enum PunchType {
        Position, Rotation, Scale
    }
}
