using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : BulletBase {
    private float speed;
    [SerializeField] protected bool canPiercing;
    [SerializeField] protected int maxPiercing = 4;
    [SerializeField] protected float reducePiercingPercent;

    protected int piercingTime;
    private System.Action<Vector3> onSpread;
    private System.Action onComplete;
    private TweenerCore<Vector3, Vector3, VectorOptions> moveTween;
    private bool canSpread;
    public override void Initalize() {
        base.Initalize();
        if (canPiercing) {
            piercingTime = 0;
            SetAlpha(1);
        }
    }

    public void SetData(float speed, System.Action<Vector3> onSpread = null) {
        this.onSpread = onSpread;
        this.speed = speed + SpeedStat.Value;
        canSpread = false;
    }

    public void Shoot(Transform target, System.Action onComplete) {
        this.onComplete = onComplete;
        moveTween?.Kill();
        moveTween = transform.DOMove(target.position, speed).SetEase(Ease.Linear).OnComplete(() => {
            canSpread = true;
            DestroyWithEffect();
        });
    }

    protected override bool IsBlockHit() {
        if (canPiercing) {
            if (maxPiercing < 0) {
                return false;
            }
            return piercingTime >= maxPiercing;
        }
        return isHitted;
    }

    protected override void Hit(Collider2D collision) {
        if (canPiercing) {
            piercingTime++;
            IHitbox victim = collision.GetComponent<IHitbox>();
            if (victim != null) {
                if (piercingTime > 1) {
                    if (reducePiercingPercent > 0) {
                        hitInfor.Damage.AddModifier(new StatModifier(-1 * reducePiercingPercent, StatModType.PercentAdd));
                    }
                }
                SetAlpha(1 - Mathf.Abs(reducePiercingPercent) * piercingTime);
                victim.TakeHit(hitInfor, transform.position);
            }
            if (piercingTime >= maxPiercing) {
                DestroyWithEffect();
                moveTween?.Kill();
            }
        }
        else {
            isHitted = true;
            IHitbox victim = collision.GetComponent<IHitbox>();
            if (victim != null) {
                victim.TakeHit(hitInfor, transform.position);
            }
            DestroyWithEffect();
            moveTween?.Kill();
        }
    }
    protected override void Destroy() {
        if (canSpread)
            onSpread?.Invoke(transform.position);
        onComplete?.Invoke();
        base.Destroy();
    }
    public override void DestroyWithEffect() {
        if (canSpread)
            onSpread?.Invoke(transform.position);
        onComplete?.Invoke();
        base.DestroyWithEffect();
    }
}
