using UnityEngine;
using Gemmob;
using System.Collections.Generic;

public class XMB02Skill01AttackComponent : MinibossAttackComponent<XMB02Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private BasicLaser bulletPrefab;
    [SerializeField] private BasicLaser waringPrefab;
    [SerializeField] private float damagePercent;
    [SerializeField] private float duration;
    [SerializeField] private float deltaShot;
    [SerializeField] private int laserCount;

    [SerializeField, Range(0, 5)] private float radius = 3f;
    [SerializeField, Range(0, 5)] private float warnignRadius = 3f;
    [SerializeField, Range(0, 1)] private float timeOffLaserPercent = 1;

    [SerializeField, Range(0f, 1f)] private float timeOffWarningLaserPercent = 0.5f;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int warningMaxStack = 4;
    [SerializeField, Range(0f, 1f)] private float warningAlpha = 0.5f;

    [SerializeField] private int numberPreload;

    private List<BasicLaser> bullets;
    private List<BasicLaser> warnings;
    private float warningTimeOffPoint;
    private int warningStack = 0;

    private bool attacking;
    private int rotateDirection;
    private Countdowner rateCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();


    public override void PreloadIngame() {
        if (bulletPrefab) {
            bulletPrefab.RegisterPool(numberPreload);
        }
        if (waringPrefab) {
            waringPrefab.RegisterPool(numberPreload);
        }
    }

    public override void StartAttack() {
        bullets?.Clear();
        warnings?.Clear();
        SpawnLaser();
        StartBeamLaser();
        delayCountdowner.StartCountdown(delayAttack);
        rotateDirection = Helper.RandomHelper.IsTrueOrFalse() ? 1 : -1;
        var per = 360 / bullets.Count;
        var offset = UnityEngine.Random.Range(0, per);
        for (int i = 0; i < bullets.Count; i++) {
            SetRotation(bullets[i].transform, offset + per * i);
        }

        for (int i = 0; i < warnings.Count; i++) {
            SetRotation(warnings[i].transform, offset + per * i);
        }
        minibossAttack.XMB02Base.XMB02Move.StartMoveAfterAttackXMB02(new Vector2(0.5f, 0.5f));
        warningStack = 0;
        attacking = false;
    }
    private void SpawnLaser() {
        bullets = new List<BasicLaser>();
        for (int i = 0; i < laserCount; i++) {
            var item = bulletPrefab.Spawn(transform);
            item.transform.localPosition = firePoint.localPosition;
            item.SetRadiusSize(radius);
            item.SetCharacterBase(minibossAttack.MinibossBase);
            bullets.Add(item);
        }

        warnings = new List<BasicLaser>();
        for (int i = 0; i < laserCount; i++) {
            var item = waringPrefab.Spawn(transform);
            item.transform.localPosition = firePoint.localPosition;
            item.SetRadiusSize(warnignRadius);
            item.StartBeam();
            item.SetAlphaLaser(warningAlpha);
            warnings.Add(item);
        }
    }
    public override void Updating() {
        if (!attacking) {
            attacking = minibossAttack.XMB02Base.XMB02Move.CompleteMoveToTarget();
        }
        else {
            if (delayCountdowner.IsCountdowning()) {
                delayCountdowner.Countdowning(Time.deltaTime);
                DrawWarning();
                minibossAttack.XMB02Base.LookTarget();
                if (delayCountdowner.IsTimeOut()) {
                    HideWarning();
                }
            }
            else {
                BeamingLaser();
            }
        }
    }

    private void DrawWarning() {
        if (delayCountdowner.Countdown < warningTimeOffPoint) {
            if (warningStack % warningMaxStack == 0) {
                foreach (var waring in warnings) {
                    waring.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
                }
            }
            warningStack++;
        }
        foreach (var waring in warnings) {
            if (delayCountdowner.Countdown < warningTimeOffPoint) {
                float percentSize = warningTimeOffPoint == 0 ? 1 : delayCountdowner.Countdown / warningTimeOffPoint;
                waring.SetPercentSize(percentSize);
            }
            waring.gameObject.SetActive(true);
            waring.Beaming(false);
        }
    }

    private void HideWarning() {
        foreach (var waring in warnings) {
            waring.SetPercentSize(0);
            waring.gameObject.SetActive(false);
        }
    }

    public override void Attacking() {
        //StartBeamLaser();
    }
    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(duration);
        deltaShotCountdowner.StartCountdown(deltaShot);
        rateCountdowner.StartCountdown(delayAttack);
        foreach (var item in bullets) {
            item.StartBeam();
            item.gameObject.SetActive(true);
        }
        foreach (var item in warnings) {
            item.StartBeam();
            item.gameObject.SetActive(true);
        }
        warningTimeOffPoint = delayAttack * (1 - timeOffWarningLaserPercent);
    }

    public void BeamingLaser() {
        if (!durationCountdowner.IsTimeOut()) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);

            float timeOffPoint = duration * (1 - timeOffLaserPercent);
            float timeOffElapsed = durationCountdowner.Countdown;
            if (timeOffElapsed < timeOffPoint) {
                float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
                foreach (var laser in bullets) {
                    laser.SetPercentSize(percentSize);

                }
            }

            RotateSefl();
            if (deltaShotCountdowner.IsTimeOut()) {
                foreach (var item in bullets) {
                    item.SetInfor((int)(minibossAttack.XMB02Base.CharacterStat.Atk.Value * damagePercent), null);
                    item.Beaming(true);
                }
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                foreach (var item in bullets) {
                    item.Beaming(false);
                }
            }
        }
        else {
            rateCountdowner.Countdowning(Time.deltaTime);
            if (rateCountdowner.IsTimeOut()) {
                durationCountdowner.StartCountdown(duration);
                rateCountdowner.StartCountdown(delayAttack);
            }
            EndAttack();
        }
    }

    public override void EndAttack() {
        EndBeamLaser();
        foreach (var item in warnings) {
            item.Recycle();
        }
        foreach (var item in bullets) {
            item.Recycle();
        }
        base.EndAttack();
    }

    private void RotateSefl() {
        minibossAttack.XMB02Base.XMB02Move.Rotate(rotateSpeed * rotateDirection);
    }

    private void SetRotation(Transform bullet, int zRotation) {
        var temp = bullet.eulerAngles;
        temp.z = zRotation;
        bullet.eulerAngles = temp;
    }
    private void EndBeamLaser() {
        if (bullets != null) {
            foreach (var item in bullets) {
                if (item != null)
                    item.EndBeam();
            }
        }
        if (warnings != null) {
            foreach (var item in warnings) {
                if (item != null) {
                    item.EndBeam();
                    item.SetRadiusSize(0);
                }
            }
        }
    }

    public override void StopAttack() {
        EndBeamLaser();
        foreach (var item in warnings) {
            item.Recycle();
        }
        foreach (var item in bullets) {
            item.Recycle();
        }
        base.StopAttack();
    }

    private void OnDisable() {
        EndBeamLaser();
    }


}