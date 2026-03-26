
using DG.Tweening;
using Gemmob;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class B15WallShield : Shield, IHitbox {
    #region Variables
    [SerializeField] private Image mainIcon;
    [SerializeField] private Image shieldIcon;
    [SerializeField] private DOTweenAnimation hitEffect;
    [SerializeField] private EnemyShieldExplosionBullet explosionBullet;
    [SerializeField] private RangeFloatValue[] spawnPosY;
    [SerializeField] private RangeFloatValue originScale;
    private IntStat maxHP = new IntStat();
    private int currentHP;
    private int damage;

    private bool isDead;
    private bool initialized;
    private Action onDie;
    private int currentAddHP = 0;
    private int currentIncreseExplosionDamage = 0;
    private int currentIncreseExplosionRadius = 0;

    public int MaxHP { get => maxHP.Value; }
    public int CurrentHP { get => currentHP; }
    #endregion

    #region Init
    public void EnableWallShield(int maxHP, int damage, int index) {
        InitData(maxHP, damage);
        gameObject.SetActive(true);
        TurnOn();
        Move();
        transform.position = Vector3.right * 15;
        transform.position += Vector3.up * spawnPosY[index].GetRandomValue();
        transform.localScale = Vector3.one * originScale.GetRandomValue();
    }
    private void InitData(int maxHP, int damage) {
        this.maxHP.SetBaseValue(maxHP);
        this.damage = damage;
        currentHP = maxHP;
        initialized = true;
        isDead = false;
    }
    #endregion

    private void Move() {
        transform.DOMoveX(-15, 5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    #region Take Damage
    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (!initialized || isDead)
            return;
        currentHP -= hit.Damage.Value;
        PlayHitEffect();
        if (currentHP < 0) {
            isDead = true;
            onDie?.Invoke();
            TurnOff();
        }
    }
    private void PlayHitEffect() {
        if (hitEffect != null) { hitEffect.DOPlay(); }
    }
    #endregion
    public override void TurnOn() {
        base.TurnOn();
        mainIcon.enabled = true;
        shieldIcon.enabled = true;
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
            hideAnimation.Play(null, true);
        }
        mainIcon.enabled = false;
        shieldIcon.enabled = false;
        DOVirtual.DelayedCall(2f, () => gameObject.Recycle());

    }
    public void DestroyImmediate() {
        gameObject.Recycle();
    }
    #region Modifier Properties
    public Transform Transform() {
        return transform;
    }

    public void SetActionOnDie() {
        onDie += ExplosionOnDie;
    }

    private void ExplosionOnDie() {
        explosionBullet.InitData();
        explosionBullet.SetExplosionDamage(damage);
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
    #endregion


    #region Trigger

    private HitInfor hitboxInfor;
    private void OnTriggerEnter2D(Collider2D collision) {
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
