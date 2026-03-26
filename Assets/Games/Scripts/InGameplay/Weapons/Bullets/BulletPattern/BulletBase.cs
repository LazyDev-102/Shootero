using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gemmob;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class BulletBase : MonoBehaviour {
    [SerializeField] protected TargetType[] targetTypes;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected ParticleSystem explosion;
    [SerializeField] private ParticleSystem selfExplosion;
    [SerializeField] private TrailRenderer myTrail;
    [Header("Preload")]
    [SerializeField] private int numberPreloadExplosion;
    [SerializeField] private int numberPreloadSelfExplosion;


    protected Action<Vector3> onDestroy;

    protected HitInfor hitInfor;
    protected FloatStat size;
    protected FloatStat speedStat;
    protected bool isHitted;
    private Collider2D hitCollider;
    private Rigidbody2D rigi;
    private bool isDestroyed;
    private float orginTrailSize = -1;

    public Rigidbody2D MyRigi {
        get {
            if (rigi == null) {
                rigi = GetComponent<Rigidbody2D>();
            }
            return rigi;
        }
    }

    public HitInfor HitInfor {
        get {
            if (hitInfor == null) {
                hitInfor = new HitInfor();
            }
            return hitInfor;
        }
    }
    public FloatStat Size {
        get {
            if (size == null) {
                size = new FloatStat();
            }
            return size;
        }
    }

    public FloatStat SpeedStat {
        get {
            if (speedStat == null) {
                speedStat = new FloatStat();
            }
            return speedStat;
        }
    }

    protected Collider2D HitCollider {
        get {
            if (hitCollider == null) {
                hitCollider = GetComponent<Collider2D>();
            }
            return hitCollider;
        }
    }

    public TrailRenderer MyTrail { get => myTrail; }

    public virtual void PreloadIngame() {
        if (explosion) {
            explosion.RegisterPool(numberPreloadExplosion);
        }
        if (selfExplosion) {
            selfExplosion.RegisterPool(numberPreloadSelfExplosion);
        }
    }

    protected virtual void OnEnable() {
        Initalize();
    }

    public virtual void Initalize() {
        isHitted = false;
        HitCollider.enabled = true;
        Size.SetBaseValue(1.0f);
        Size.Reset();
        SpeedStat.SetBaseValue(0.0f, true);
        onDestroy = null;
        isDestroyed = false;

        ShowMoveTrail();
    }

    public void SetAlpha(float alpha) {
        if (sprite) {
            sprite.ChangeAlpha(alpha);
        }
    }

    public void SetHitInfor(int atk, List<IEffectAttackModable> effects, ObjectBase causer, int critChance = 0, float critDamage = 0) {
        int damage = atk;
        HitInfor.SetInfor(damage, effects, causer, critChance, critDamage);
    }

    public void SetSize(float size) {
        this.size.SetBaseValue(size);
        if (myTrail) {
            if (orginTrailSize < 0) {
                orginTrailSize = myTrail.widthMultiplier;
            }
            myTrail.widthMultiplier = orginTrailSize * size;
        }

        ChangeSize();
    }

    public void ChangeSize() {
        transform.localScale = Vector3.one * this.size.Value;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Respawn)) {
            Destroy();
        }
        if (IsBlockHit()) {
            return;
        }
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                Hit(collision);
                return;
            }
        }
    }

    protected virtual bool IsBlockHit() {
        return isHitted;
    }

    protected virtual void Hit(Collider2D collision) {
        isHitted = true;
        HitCollider.enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(HitInfor, transform.position);
        }
        DestroyWithEffect();
    }

    public virtual void DestroyWithEffect() {
        if (isDestroyed)
            return;
        isDestroyed = true;
        if (explosion != null) {
            GameManager.Instance.GameLoader.SpawnEffectExplosion(explosion, transform.position);
        }
        onDestroy?.Invoke(transform.position);
        RemoveMe();
    }

    protected virtual void Destroy() {
        if (isDestroyed)
            return;
        isDestroyed = true;
        onDestroy?.Invoke(transform.position);
        RemoveMe();
    }

    public virtual void SelfDestruction() {
        if (isDestroyed)
            return;
        isDestroyed = true;
        if (selfExplosion != null) {
            GameManager.Instance.GameLoader.SpawnEffectExplosion(selfExplosion, transform.position);
        }
        onDestroy?.Invoke(transform.position);
        RemoveMe();
    }

    protected virtual void RemoveMe() {
        HideMoveTrail();
        RemoveAllOnDestroy();
        GameManager.Instance.GameLoader.RemoveBullet(this);
    }

    public void AddOnDestroy(Action<Vector3> onDestroy) {
        this.onDestroy += onDestroy;
    }

    public void RemoveOnDestroy(Action<Vector3> onDestroy) {
        this.onDestroy -= onDestroy;
    }

    public void RemoveAllOnDestroy() {
        onDestroy = null;
    }

    public void HideMoveTrail() {
        if (myTrail) {
            myTrail.HideTrail();
        }
    }

    public void ShowMoveTrail() {
        if (myTrail) {
            myTrail.ShowTrail();
        }
    }
}
