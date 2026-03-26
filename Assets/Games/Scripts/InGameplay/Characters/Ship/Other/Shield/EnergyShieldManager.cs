using Gemmob;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldManager : Shield, IHitbox {

    [SerializeField] private EnergyShieldExplosionBullet explosionBullet;
    [SerializeField] private CircleCollider2D myCollider;
    [SerializeField] private SpriteRenderer icon;
    private IntStat maxHP = new IntStat();
    private int currentHP;
    private float explosionRadius;
    private float dodgeRate = 0;
    private float timeReborn = 10;
    private int damageHit = 0;
    private int projectileCount = 3;

    private HitInfor hit;
    private bool isDead;
    private bool initialized;
    private Action onDie;
    private Action onDisableShield;
    private Action onReborn;
    private int currentAddHP = 0;
    private int currentIncreseExplosionDamage = 0;
    private int currentIncreseExplosionRadius = 0;
    private Countdowner rebornCountdowner = new Countdowner();

    public int MaxHP { get => maxHP.Value; }
    public int CurrentHP { get => currentHP; }

    private Transform target;

    public void EnableEnergyShield(int maxHP, float dodgeRate, float timeReborn, Action onDisableShield, Action onReborn, Transform target) {
        InitData(maxHP, dodgeRate, timeReborn, onDisableShield, onReborn, target);
        gameObject.SetActive(true);
        ChangeState(true);
        TurnOn();
    }
    private void InitData(int maxHP, float dodgeRate, float timeReborn, Action onDisableShield, Action onReborn, Transform target) {
        this.maxHP.SetBaseValue(maxHP);
        this.dodgeRate = dodgeRate;
        this.timeReborn = timeReborn;
        this.onDisableShield = onDisableShield;
        this.onReborn = onReborn;
        this.target = target;
        currentHP = maxHP;
        initialized = true;
        rebornCountdowner.StartCountdown(timeReborn);
    }
    private void ChangeState(bool state) {
        myCollider.enabled = state;
        icon.enabled = state;
    }
    private void Update() {
        if (!initialized)
            return;
        if (!isDead)
            return;
        if (rebornCountdowner.IsTimeOut()) {
            isDead = false;
            currentHP = MaxHP;
            ChangeState(true);
            rebornCountdowner.StartCountdown(timeReborn);
            onReborn?.Invoke();
            TurnOn();
        }
        else {
            rebornCountdowner.Countdowning(Time.deltaTime);
        }
    }
    public void SetDodgeRate(float percent, bool reset = true) {
        dodgeRate = reset ? percent : dodgeRate + percent;
    }

    private int CaculateDamage(int damage) {
        if (damage < 0)
            return 0;

        var defaultDamage = damage;
        damage -= (int)(damage * dodgeRate);
        return defaultDamage > damage ? damage : defaultDamage;
    }

    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (!initialized || isDead)
            return;
        this.hit = hit;
        damageHit = CaculateDamage(hit.Damage.Value);
        currentHP -= damageHit;
        if (currentHP < 0) {
            isDead = true;
            onDie?.Invoke();
            ChangeState(false);
            onDisableShield?.Invoke();
            TurnOff();
        }
        EventDispatcher.Instance.Dispatch(new EventKey.OnEnergyShieldHitDamage() { Causer = hit.Causer, /*shieldType = ShieldType.EnergyShield,*/ CurrentHP = currentHP, Target = target });
    }

    public Transform Transform() {
        return transform;
    }

    public void SetActionOnDie() {
        onDie += ExplosionOnDie;
    }

    private void ExplosionOnDie() {
        explosionBullet.InitData();
        if (gameObject.activeInHierarchy)
            StartCoroutine(IETurnOffExplosion());
    }

    private IEnumerator IETurnOffExplosion() {
        yield return Yielder.Wait(1f);
        explosionBullet.gameObject.SetActive(false);
    }

    public void SetExplosionDamage(StatModifier[] damageModifier) {
        if (currentIncreseExplosionDamage == damageModifier.Length)
            return;

        explosionBullet.SetExplosionDamage(damageModifier[currentIncreseExplosionDamage]);
        currentIncreseExplosionDamage++;
    }

    public void SetExplosionRadius(StatModifier[] radiusModifier) {
        if (currentIncreseExplosionRadius == radiusModifier.Length)
            return;

        explosionBullet.SetExplosionRadius(radiusModifier[currentIncreseExplosionRadius]);
        currentIncreseExplosionRadius++;
    }
    public void AddHP(StatModifier[] modifier) {
        if (currentAddHP == modifier.Length)
            return;

        maxHP.AddModifier(modifier[currentAddHP]);
        currentAddHP++;
    }
    public override void TurnOff() {
        if (shieldCollider != null) {
            shieldCollider.enabled = false;
        }
        if (showEffect && showEffect.isPlaying) {
            showEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (hideEffect) {
            hideEffect.Play();
        }
        if (hideAnimation) {
            hideAnimation.Play(() => {
                ChangeState(false);
            }, true);
        }
        else {
            ChangeState(false);
        }
    }
}
