using DG.Tweening;
using Gemmob;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WallShieldManager : Shield, IHitbox {
    #region Variables
    [SerializeField] private EnergyShieldExplosionBullet explosionBullet;
    [SerializeField] private Image icon;
    [SerializeField] private bool status;//Test Size
    private IntStat maxHP = new IntStat();
    private int currentHP;
    private int damage;
    private float timeReborn = 10;

    private float speed;
    private float maxWidth;
    private bool isDead;
    private bool initialized;
    private Action onDie;
    private int currentAddHP = 0;
    private int currentIncreseExplosionDamage = 0;
    private int currentIncreseExplosionRadius = 0;
    private Countdowner rebornCountdowner = new Countdowner();

    public int MaxHP { get => maxHP.Value; }
    public int CurrentHP { get => currentHP; }
    #endregion

    #region Init
    public void EnableWallShield(int maxHP, int damage, float timeReborn, float speed) {
        if (!status)
            return;
        InitData(maxHP, damage, timeReborn, speed);
        gameObject.SetActive(true);
        ChangeState(true);
        TurnOn();
        Move();
    }
    public WallShieldManager Active(bool status) {
        this.status = status;
        return this;
    }
    private void InitData(int maxHP, int damage, float timeReborn, float speed) {
        if (!status)
            return;
        this.maxHP.SetBaseValue(maxHP);
        this.damage = damage;
        this.timeReborn = timeReborn;
        this.speed = speed;
        currentHP = maxHP;
        initialized = true;
        rebornCountdowner.StartCountdown(timeReborn);
    }
    private void ChangeState(bool state) {
        if (!status)
            return;
        shieldCollider.enabled = state;
        icon.enabled = state;
    }
    #endregion

    #region Update
    private void OnEnable() {
        gameObject.SetActive(status);
    }
    private void Update() {
        if (!status)
            return;
        if (!initialized)
            return;
        if (!isDead)
            return;
        if (rebornCountdowner.IsTimeOut()) {
            isDead = false;
            currentHP = MaxHP;
            ChangeState(true);
            rebornCountdowner.StartCountdown(timeReborn);
            TurnOn();
        }
        else {
            rebornCountdowner.Countdowning(Time.deltaTime);
        }
    }
    private void Move() {
        if (!status)
            return;
        transform.DOMoveX(-10, speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetUpdate(false);
    }
    #endregion

    #region Take Damage
    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (!status)
            return;
        if (!initialized || isDead || hit == null)
            return;
        currentHP -= hit.Damage.Value;
        if (currentHP < 0) {
            isDead = true;
            onDie?.Invoke();
            ChangeState(false);
            TurnOff();
        }
        EnergyOnHPChanged();
    }
    #endregion

    #region Modifier Properties
    public Transform Transform() {
        return transform;
    }

    public void SetActionOnDie(float percentDamage, float radius) {
        if (!status)
            return;
        explosionBullet.SetExplosionDamageBase((int)(percentDamage * GameManager.Instance.GameLoader.Ship.ShipStat.Atk.Value));
        explosionBullet.SetExplosionRadiusBase(radius);
        onDie += ExplosionOnDie;
    }

    private void ExplosionOnDie() {
        if (!status)
            return;
        explosionBullet.InitData();
        if (gameObject.activeInHierarchy)
            StartCoroutine(IETurnOffExplosion());
    }

    private IEnumerator IETurnOffExplosion() {
        yield return Yielder.Wait(1f);
        explosionBullet.gameObject.SetActive(false);
    }

    public void SetExplosionDamage(StatModifier[] damageModifier) {
        if (!status)
            return;
        if (currentIncreseExplosionDamage == damageModifier.Length)
            return;

        explosionBullet.SetExplosionDamage(damageModifier[currentIncreseExplosionDamage]);
        currentIncreseExplosionDamage++;
    }

    public void SetExplosionRadius(StatModifier[] radiusModifier) {
        if (!status)
            return;
        if (currentIncreseExplosionRadius == radiusModifier.Length)
            return;

        explosionBullet.SetExplosionRadius(radiusModifier[currentIncreseExplosionRadius]);
        currentIncreseExplosionRadius++;
    }
    public void AddHP(StatModifier[] modifier) {
        if (!status)
            return;
        if (currentAddHP == modifier.Length)
            return;

        maxHP.AddModifier(modifier[currentAddHP]);
        currentAddHP++;
    }
    #endregion

    #region On,Off Shield
    public override void TurnOn() {
        if (!status)
            return;
        base.TurnOn();
        TurnOnEnergyHpBar();
    }
    public override void TurnOff() {
        if (!status)
            return;
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
    #endregion

    #region HPBar
    [SerializeField] private Image wallShieldProgressBG;
    [SerializeField] private Image wallShieldProgress;
    public void TurnOnEnergyHpBar() {
        if (!status)
            return;
        wallShieldProgressBG.gameObject.SetActive(true);
        wallShieldProgress.gameObject.SetActive(true);
        wallShieldProgressBG.fillAmount = 1;
        wallShieldProgress.fillAmount = 1;
    }
    public void TurnOffEnergyHpBar() {
        if (!status)
            return;
        wallShieldProgressBG.gameObject.SetActive(false);
        wallShieldProgress.gameObject.SetActive(false);
    }
    private void EnergyOnHPChanged() {
        if (!status)
            return;
        float ratio = (float)((float)CurrentHP / (float)MaxHP);
        if (ratio <= 0) {
            TurnOffEnergyHpBar();
            return;
        }
        if (ratio > 1)
            ratio = 1;
        EnergyShieldHPBarFill(ratio);
    }
    private void EnergyShieldHPBarFill(float ratio) {
        if (!status)
            return;
        wallShieldProgress.fillAmount = ratio;
    }
    #endregion

    #region Trigger

    private HitInfor hitboxInfor;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!status)
            return;
        if (collision.tag.Equals(GameTag.Enemy)) {
            IHitbox takeHit = collision.GetComponent<IHitbox>();
            if (takeHit != null) {
                takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
                if (takeHit is EnemyHitbox eHit) {
                    eHit.EnemyBase.EnemyMove.Knockback(transform.position);
                }
            }
        }
    }
    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, null);
        return hitboxInfor;
    }
    #endregion
}
