using UnityEngine;
using System;
using System.Collections;
using Gemmob;
using System.Collections.Generic;

public class XB01Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private XB01Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private BasicLaser bulletPrefab;
    [SerializeField] private BasicLaser waringPrefab;

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
    private AttackData attackData;
    private int rotateDirection;
    private Countdowner rateCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void PreloadIngame() {
        if (bulletPrefab) {
            bulletPrefab.RegisterPool(numberPreload);
        }
        if (waringPrefab) {
            waringPrefab.RegisterPool(numberPreload);
        }
    }

    public override void StartAttack() {
        attackData = CurAttackData;
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
        bossAttack.XB01Base.XB01Move.StartMoveAfterAttackXB01(new Vector2(0.5f, 0.5f));
        warningStack = 0;
        attacking = false;
    }
    private void SpawnLaser() {
        bullets = new List<BasicLaser>();
        for (int i = 0; i < attackData.LaserCount; i++) {
            var item = bulletPrefab.Spawn(transform);
            item.transform.localPosition = firePoint.localPosition;
            item.SetRadiusSize(radius);
            item.SetCharacterBase(bossAttack.BossBase);
            bullets.Add(item);
        }

        warnings = new List<BasicLaser>();
        for (int i = 0; i < attackData.LaserCount; i++) {
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
            attacking = bossAttack.XB01Base.XB01Move.CompleteMoveToTarget();
        }
        else {
            if (delayCountdowner.IsCountdowning()) {
                delayCountdowner.Countdowning(Time.deltaTime);
                DrawWarning();
                bossAttack.XB01Base.LookTarget();
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
            waring.gameObject.SetActive(false);
        }
    }

    public override void Attacking() {
        //StartBeamLaser();
    }
    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(attackData.Duration);
        deltaShotCountdowner.StartCountdown(attackData.DeltaShot);
        rateCountdowner.StartCountdown(delayAttack);
        foreach (var item in bullets) {
            item.StartBeam();
            item.gameObject.SetActive(true);
        }
        warningTimeOffPoint = delayAttack * (1 - timeOffWarningLaserPercent);
    }

    public void BeamingLaser() {
        if (!durationCountdowner.IsTimeOut()) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);

            float timeOffPoint = attackData.Duration * (1 - timeOffLaserPercent);
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
                    item.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
                    item.Beaming(true);
                }
                deltaShotCountdowner.StartCountdown(attackData.DeltaShot);
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
                durationCountdowner.StartCountdown(attackData.Duration);
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
        bossAttack.BossBase.BossMove.Rotate(rotateSpeed * rotateDirection);
    }

    private void SetRotation(Transform bullet, int zRotation) {
        var temp = bullet.eulerAngles;
        temp.z = zRotation;
        bullet.eulerAngles = temp;
    }
    private void EndBeamLaser() {
        if (bullets == null)
            return;
        foreach (var item in bullets) {
            if (item != null)
                item.EndBeam();
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

    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float duration;
        [SerializeField] private float deltaShot;
        [SerializeField] private int laserCount;

        public float DamagePercent {
            get => damagePercent;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float Duration {
            get => duration;
        }
        public float LaserCount {
            get => laserCount;
        }
    }
}
