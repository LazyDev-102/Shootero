
using UnityEngine;

public class PierceSinFrontBullet : SinFrontBullet {
    [SerializeField] protected int maxPiercing = 4;
    [SerializeField] protected float reducePiercingPercent;
    [SerializeField] protected TargetType[] blockTypes;
    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected bool isFading;
    [SerializeField] protected float timeFade;


    protected int piercingTime;
    protected float fadingTime;

    public override void Initalize() {
        base.Initalize();
        piercingTime = 0;
        fadingTime = 0;
        SetAlpha(1);
    }

    public override void SetTimeFading(float time) {
        if (isFading)
            timeFade = time;
    }
    protected override bool IsBlockHit() {
        if (maxPiercing < 0) {
            return false;
        }
        return piercingTime >= maxPiercing;
    }
    protected override void FixedUpdate() {
        if (isFading && sprite) {
            fadingTime += Time.fixedDeltaTime / timeFade;
            var alpha = 1 - fadingTime + startFadeAt;
            SetAlpha(alpha);
            if (alpha <= 0)
                Destroy();
        }
        base.FixedUpdate();
    }
    protected override void Hit(Collider2D collision) {
        if (isFading)
            HitDamageWithFade(collision);
        else
            HitDamageNormal(collision);
    }
    protected virtual void HitDamageNormal(Collider2D collision) {
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
            if (explosion != null && hitEffect != null) {
                GameManager.Instance.GameLoader.SpawnEffectExplosion(hitEffect, transform.position);
            }
        }
        if (piercingTime >= maxPiercing) {
            DestroyWithEffect();
        }
    }
    protected virtual void HitDamageWithFade(Collider2D collision) {
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            hitInfor.Damage.SetBaseValue((int)(hitInfor.Damage.Value * sprite.color.a), true);
            victim.TakeHit(hitInfor, transform.position);
            if (explosion != null && hitEffect != null) {
                GameManager.Instance.GameLoader.SpawnEffectExplosion(hitEffect, transform.position);
            }
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Respawn)) {
            Destroy();
        }
        foreach (var target in blockTypes) {
            if (collision.CompareTag(target.ToString())) {
                DestroyWithEffect();
            }
        }
        base.OnTriggerEnter2D(collision);
    }
}
