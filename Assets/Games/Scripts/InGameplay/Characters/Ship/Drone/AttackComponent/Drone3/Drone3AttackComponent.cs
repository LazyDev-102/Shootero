using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3AttackComponent : DroneAttackComponent {
    [SerializeField] private Drone03Stat droneStat;
    [SerializeField] private BasicLaser bullet;
    [SerializeField] private BasicLaser warningLine1;
    [SerializeField, Range(10, 100)] private int laserLength = 20;
    [SerializeField, Range(0f, 5f)] private float radiusSize = 0.5f;
    [SerializeField] private float shotDuration;
    [SerializeField] private float deltaShot;
    [SerializeField] private float rateTime;
    [SerializeField] private float delayAttack = 1;
    [SerializeField] DroneBase droneBase;
    [SerializeField, Range(0f, 1f)] private float warningAlpha = 0.5f;
    [SerializeField, Range(0f, 1f)] private float timeOffWarningLaserPercent = 0.5f;
    [SerializeField] private int warningMaxStack = 4;

    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner delayAttackCD = new Countdowner();
    private float warningTimeOffPoint;
    private int warningStack = 0;
    public override void PreloadIngame() {
    }

    public override void Initialize() {
        base.Initialize();
        attackCountdowner.StartCountdown(FireRate);
        StartBeamLaser();
    }
    public override void Attack() {
        if (!canAttack) {
            bullet.EndBeam();
            warningLine1.EndBeam();
            return;
        }
        if (delayAttackCD.IsCountdowning()) {
            DrawWarning();
            delayAttackCD.Countdowning(Time.deltaTime);
            if (delayAttackCD.IsTimeOut()) {
                warningLine1.gameObject.SetActive(false);
            }
        }
        else {
            if (!durationCountdowner.IsTimeOut()) {
                durationCountdowner.Countdowning(Time.deltaTime);
                deltaShotCountdowner.Countdowning(Time.deltaTime);
                if (deltaShotCountdowner.IsTimeOut()) {
                    bullet = ChangingLaserBullet(bullet);
                    bullet.Beaming(true);
                    deltaShotCountdowner.StartCountdown(deltaShot);
                }
                else {
                    bullet.Beaming(false);
                }
            }
            else {
                bullet.EndBeam();
                attackCountdowner.Countdowning(Time.deltaTime);
                if (attackCountdowner.IsTimeOut()) {
                    durationCountdowner.StartCountdown(droneStat.LaserDuration.Value);
                    attackCountdowner.StartCountdown(FireRate);
                    delayAttackCD.StartCountdown(delayAttack);
                    warningLine1.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnDisable() {
        EndBeamLaser();
    }

    public override void Updating() {
        base.Updating();
        Attack();
    }

    private void DrawWarning() {
        if (delayAttackCD.Countdown < warningTimeOffPoint) {
            //float percentSize = warningTimeOffPoint == 0 ? 1 : delayAttackCD.Countdown / warningTimeOffPoint;
            //warningLine1.SetPercentSize(percentSize);
            if (warningStack % warningMaxStack == 0) {
                warningLine1.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
            }
            warningStack++;

        }
        warningLine1.gameObject.SetActive(true);
        warningLine1.Beaming(false);
    }

    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(droneStat.LaserDuration.Value);
        deltaShotCountdowner.StartCountdown(deltaShot);
        attackCountdowner.StartCountdown(FireRate);
        delayAttackCD.StartCountdown(delayAttack);
        bullet.StartBeam();
        bullet.SetMaxLength(laserLength);
        bullet.SetRadiusSize(radiusSize);
        bullet.gameObject.SetActive(true);
        warningLine1.StartBeam();
        warningLine1.SetMaxLength(laserLength);
        warningLine1.SetRadiusSize(radiusSize);
        warningLine1.SetAlphaLaser(warningAlpha);
        warningTimeOffPoint = delayAttack * (1 - timeOffWarningLaserPercent);
    }

    private void EndBeamLaser() {
        bullet.gameObject.SetActive(false);
        bullet.EndBeam();
        warningLine1.gameObject.SetActive(false);
        warningLine1.EndBeam();
    }
}
