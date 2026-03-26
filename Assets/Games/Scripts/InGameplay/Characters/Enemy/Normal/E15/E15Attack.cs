
using DG.Tweening;
using UnityEngine;

public class E15Attack : EnemyAttack {
    private E15Base e15Base;
    public E15Base E15Base {
        get {
            if (e15Base == null) {
                e15Base = EnemyBase as E15Base;
            }
            return e15Base;
        }
    }

    #region Attack
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PierceLaser bullet;
    [SerializeField] private BasicLaser warningLine;
    [SerializeField] private ParticleSystem bulletEffect;
    [SerializeField] private int bulletLength;
    [SerializeField] private StatModifier percentDamage;
    [SerializeField] private StatModifier duration;
    [SerializeField] private StatModifier attackSpeed;
    [SerializeField] private AnimationCurve attackMoveCurve;

    [SerializeField, Range(0f, 1f)] private float warningAlpha = 0.5f;
    [SerializeField, Range(0f, 1f)] private float timeOffWarningLaserPercent = 0.5f;

    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();
    private Countdowner endAttackCD = new Countdowner();
    private float warningTimeOffPoint;
    private int warningStack = 0;
    private int warningMaxStack = 4;
    private float bulletRadius = 1;
    private bool canAttack;
    public override void Initialize() {
        base.Initialize();
        canAttack = false;
        HideWarning();
    }
    protected override void StartAttack() {
        StartBeamLaser();
        delayCountdowner.StartCountdown(delayAttack);
        endAttackCD.StartCountdown(0.1f);
        bullet.SetMaxLength(bulletLength);
        bullet.StartBeam();
        warningLine.SetMaxLength(bulletLength);
        warningLine.StartBeam();
        warningLine.SetAlphaLaser(warningAlpha);
        warningStack = 0;
        isAttacking = true;
        canAttack = false;
    }
    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(duration.Value);
        deltaShotCountdowner.StartCountdown(0);
        bullet.StartBeam();
        bullet.SetRadiusSize(bulletRadius);
        bullet.gameObject.SetActive(true);
        warningTimeOffPoint = delayAttack * (1 - timeOffWarningLaserPercent);
        bullet.SetInfor((int)(E15Base.E15Stat.Atk.Value * percentDamage.Value), null);
    }

    private void DrawWarning() {
        if (delayCountdowner.Countdown < warningTimeOffPoint) {
            float percentSize = warningTimeOffPoint == 0 ? 1 : delayCountdowner.Countdown / warningTimeOffPoint;
            warningLine.SetPercentSize(percentSize);
            if (warningStack % warningMaxStack == 0) {
                warningLine.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
            }
            warningStack++;
        }
        warningLine.gameObject.SetActive(true);
        warningLine.Beaming(false);
    }

    private void HideWarning() {
        warningLine.gameObject.SetActive(false);

    }
    protected override void Attacking() {
        warningLine.SetMaxLength(bulletLength);
        MoveToTargetPoint();
    }
    public override void Updating() {
        if (CanAttack()) {
            if (delayCountdowner.IsCountdowning()) {
                delayCountdowner.Countdowning(Time.deltaTime);
                DrawWarning();
                E15Base.LookTarget();
                if (delayCountdowner.IsTimeOut()) {
                    HideWarning();
                }
            }
            else {
                bullet.Beaming(true);
                canAttack = false;
                PlayEffectBullet();
                DOVirtual.DelayedCall(duration.Value, () => EndAttack());
            }
        }
    }
    private void MoveToTargetPoint() {
        var targetPoint = new Vector3(Target.transform.position.x + Random.Range(-1f, 1f), Target.transform.position.y + bulletLength, 0);
        transform.DOMove(targetPoint, 5f / attackSpeed.Value).SetEase(attackMoveCurve).OnComplete(() => canAttack = true);
    }
    private void PlayEffectBullet() {
        bulletEffect.Play();
    }

    public override bool CanAttack() {
        return canAttack;
    }
    public override void EndAttack() {
        EndBeamLaser();
        base.EndAttack();
    }

    private void EndBeamLaser() {
        bullet.EndBeam();
        bullet.gameObject.SetActive(false);
        warningLine.gameObject.SetActive(false);
        canAttack = false;
    }
    #endregion
}
