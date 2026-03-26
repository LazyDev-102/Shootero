using UnityEngine;

public class PierceFrontBullet : FrontBullet {
    [SerializeField] protected int maxPiercing = 4;
    [SerializeField] protected float reducePiercingPercent;
    [SerializeField] protected TargetType[] blockTypes;
    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected bool isFading;
    [SerializeField] protected float startFadeAt;

    protected float timeFade;
    protected int piercingTime;
    protected float fadingTime;

    public override void Initalize() {
        base.Initalize();
        piercingTime = 0;
        fadingTime = 0;
        SetAlpha(1);
        SetMaxPierce();
    }
    public override void Shoot(float speed, Vector2 direction, float acceleration = 0, float minSpeed = float.MinValue) {
        base.Shoot(speed, direction, acceleration, minSpeed);
        SetMaxPierce();
    }
    private void SetMaxPierce() {
        if (!GameManager.Initialized) {
            maxPiercing = 1;
        }
        else {
            ShipBase ship = GameManager.Instance.GameLoader.Ship;
            if (ship) {
                maxPiercing = ship.ShipStat.PierceStack.Value + 1;
            }
        }
    }
    public override void SetTimeFading(float time) {
        if (isFading) {
            timeFade = time;
            fadingTime = 0;
            piercingTime = 0;
        }
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
            if (alpha > 1)
                alpha = 1;
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
            int damageValue = (int)(hitInfor.Damage.Value * sprite.color.a);
            if (damageValue <= 0)
                damageValue = 1;
            hitInfor.Damage.SetBaseValue(damageValue, true);
            victim.TakeHit(hitInfor, transform.position);
            if (explosion != null && hitEffect != null) {
                GameManager.Instance.GameLoader.SpawnEffectExplosion(hitEffect, transform.position);
            }
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        foreach (var target in blockTypes) {
            if (collision.CompareTag(target.ToString())) {
                DestroyWithEffect();
            }
        }
        base.OnTriggerEnter2D(collision);
    }
}
