

using System;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class Explosioner : MonoBehaviour {
    [SerializeField] private TargetType[] targetTypes;
    [SerializeField] protected ParticleSystem explosion;
    [SerializeField] private float lifeTime;
    public Action<Vector3> onDestroy;

    private Countdowner lifeTimeCountdowner;
    protected HitInfor hitInfor;
    protected Collider2D myCollider;
    private bool isExplosing;

    public Collider2D MyCollider {
        get {
            if (myCollider == null) {
                myCollider = GetComponent<Collider2D>();
            }
            return myCollider;
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

    public virtual void Initialize() {

    }

    public virtual void Destroyed() {

    }

    public virtual void PreloadIngame() {

    }

    public Explosioner Explosioning() {
        if (explosion) {
            explosion.Play();
        }
        MyCollider.enabled = true;
        lifeTimeCountdowner.StartCountdown(lifeTime);
        isExplosing = true;
        return this;
    }

    public Explosioner SetHitInfor(int damage, List<IEffectAttackModable> effects, ObjectBase causer) {
        HitInfor.SetInfor(damage, effects, causer);
        return this;
    }

    public Explosioner SetRadius(float radius) {
        transform.Scale(radius);
        return this;
    }
    public Explosioner SetRadiusEffect(float radius) {
        var pars = GetComponentsInChildren<ParticleSystem>();
        foreach (var item in pars) {
            var x = item.main;
            x.startSize = x.startSize.constantMax * radius;
        }
        return this;
    }
    public Explosioner AddOnDestroy(Action<Vector3> onDestroy) {
        this.onDestroy = onDestroy;
        return this;
    }
    private void Update() {
        if (isExplosing) {
            if (lifeTimeCountdowner.IsTimeOut()) {
                MyCollider.enabled = false;
                isExplosing = false;
                Destroy();
            }
            lifeTimeCountdowner.Countdowning(Time.deltaTime);
        }
    }

    private void Destroy() {
        onDestroy?.Invoke(transform.position);
        GameManager.Instance.GameLoader.DespawnExplosion(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                IHitbox victim = collision.GetComponent<IHitbox>();
                if (victim != null) {
                    victim.TakeHit(HitInfor, transform.position);
                }
            }
        }
    }
}
