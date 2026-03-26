using System.Collections;
using UnityEngine;
using Helper;
using Gemmob;

public class MB12Skill01AttackComponent : MinibossAttackComponent<MB12Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bullet;
    [SerializeField] private RangeFloatValue rangeSpreadAngle;
    [SerializeField] private float deltaShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float duration;
    [SerializeField] private float acceleration;
    [SerializeField] private int numberPreload;

    private Countdowner durationCD = new Countdowner();
    private Countdowner deltaShotCD = new Countdowner();
    private Countdowner delayAttackCoundowner = new Countdowner();
    private bool hasAttack;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

    }

    public override void Initialize() {
    }
    public override void Attacking() {
        durationCD.StartCountdown(duration);
        deltaShotCD.StartCountdown(0);
        delayAttackCoundowner.StartCountdown(delayAttack);
        hasAttack = false;
    }

    private void Shotting() {
        if (hasAttack)
            return;
        delayAttackCoundowner.Countdowning(Time.deltaTime);
        if (delayAttackCoundowner.IsTimeOut()) {
            Vector2 directionShotBase = firePoint.up;
            durationCD.Countdowning(Time.deltaTime);
            if (durationCD.IsCountdowning()) {
                deltaShotCD.Countdowning(Time.deltaTime);
                if (deltaShotCD.IsTimeOut()) {
                    Vector2 directionShot = directionShotBase.RotateDirection(rangeSpreadAngle.GetRandomValue());
                    RotateFrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                    if (newBullet) {
                        newBullet = ChangingBullet(newBullet);
                        newBullet.SetInfo(0.1f);
                        newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                        newBullet.Shoot(directionShot, bulletSpeed, acceleration);
                    }
                    deltaShotCD.StartCountdown(deltaShot);
                }
            }
            else {
                EndAttack();
                hasAttack = true;
            }
        }
    }


    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB12Base.MB12Stat.Atk.Value * damagePercent);
        hasAttack = false;
    }

    public override void Updating() {
        minibossAttack.MB12Base.LookTarget();
        Shotting();
    }
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB12Base);
        return bullet;
    }

}