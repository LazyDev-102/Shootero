using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Gemmob;
using DG.Tweening;

public class AuraOffenseManager : Shield, IHitbox {
    [SerializeField] private CircleCollider2D myCollider;
    [SerializeField] private Transform iconTrans;
    [SerializeField] private ParticleSystem durationEffect;

    private Countdowner deltaCD = new Countdowner();
    private bool initialized;
    private bool canAttack;
    private HitInfor hitboxInfor;
    private float deltaAttack;
    private float percentDamage;


    public AuraOffenseManager InitData(float deltaAttack, float radius) {
        this.deltaAttack = deltaAttack;
        myCollider.enabled = false;
        myCollider.radius = radius;
        percentDamage = 0.1f;
        return this;
    }

    public override void TurnOn() {
        base.TurnOn();
        deltaCD.StartCountdown(0);
        initialized = true;
        canAttack = true;
        DOVirtual.DelayedCall(1f, () => durationEffect?.Play());
    }
    public override void TurnOff() {
        base.TurnOff();
        initialized = false;
    }

    public void ChangeRadius(float percentRadiusModifier) {
        iconTrans.localScale += new Vector3(iconTrans.localScale.x * percentRadiusModifier, iconTrans.localScale.y * percentRadiusModifier, iconTrans.localScale.z * percentRadiusModifier);
        myCollider.radius += percentRadiusModifier * myCollider.radius;
        var effectRadius = durationEffect.shape;
        effectRadius.radius += percentRadiusModifier * durationEffect.shape.radius;
    }

    public void ChangeDamage(float percentDamage) {
        this.percentDamage += percentDamage;
    }
    public void ChangeDeltaShot(float percentDeltaShot) {
        deltaAttack *= (1 - percentDeltaShot);
    }

    private void Update() {
        if (initialized) {
            if (deltaCD.IsTimeOut()) {
                if (gameObject.activeInHierarchy)
                    StartCoroutine(Attack());
                deltaCD.StartCountdown(deltaAttack);
            }
            else {
                deltaCD.Countdowning(Time.deltaTime);
            }
        }
    }
    IEnumerator Attack() {
        canAttack = true;
        myCollider.enabled = true;
        yield return Yielder.Wait(0.1f);
        myCollider.enabled = false;
        canAttack = false;
    }
    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (!canAttack)
            return;
        EventDispatcher.Instance.Dispatch(new EventKey.OnAuraHitDamage() { Hit = hit, PercentDamage = percentDamage });
    }

    public Transform Transform() {
        return transform;
    }

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, null);
        return hitboxInfor;
    }
}