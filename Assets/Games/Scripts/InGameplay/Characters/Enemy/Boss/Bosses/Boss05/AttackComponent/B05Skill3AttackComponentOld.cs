using UnityEngine;
using System;

public class B05Skill3AttackComponentOld : BossSkillAttackComponent {
    [SerializeField] private B05Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Laser warningLaser;
    [SerializeField] private Laser bulletLaser;

    private Countdowner totalTimeCountdowner = new Countdowner();
    private Countdowner fireRateCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner warningCountdowner = new Countdowner();
    private Countdowner durationPerAttackCountdowner = new Countdowner();
    private bool attacking;
    private AttackData CurAttackData {
        get {
            return attackDatas[CurrentPhaseIndex];
        }
    }
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
    }
    public override void Updating() {
        BeamingLaser();
    }
    public override void Attacking() {
        SetTimeCountdown();
        StartBeamLaser();
    }
    private void SetTimeCountdown() {
        totalTimeCountdowner.StartCountdown(CurAttackData.TotalAttackTime);
        fireRateCountdowner.StartCountdown(CurAttackData.FireRate);
        durationPerAttackCountdowner.StartCountdown(CurAttackData.DurationPerAttack);
        deltaShotCountdowner.StartCountdown(0);
        warningCountdowner.StartCountdown(CurAttackData.WarningTime);
    }
    private void StartBeamLaser() {
        bulletLaser.StartBeam();
        warningLaser.StartBeam();
        bulletLaser.gameObject.SetActive(true);
        warningLaser.gameObject.SetActive(true);
    }

    public void BeamingLaser() {
        if (!totalTimeCountdowner.IsTimeOut() || attacking) { // Nếu như chưa hết tổng thời gian skill = 10s
            totalTimeCountdowner.Countdowning(Time.deltaTime);
            if (!fireRateCountdowner.IsTimeOut()) { //nếu như chưa hết 1 lượt bắn
                attacking = true;
                fireRateCountdowner.Countdowning(Time.deltaTime);
                if (!warningCountdowner.IsTimeOut()) {  // nếu đang warning target
                    warningCountdowner.Countdowning(Time.deltaTime);
                    bossAttack.B05Base.LookTarget();
                    warningLaser.Beaming(false);
                }
                else { // Hết warning
                    warningLaser.gameObject.SetActive(false);
                    if (!durationPerAttackCountdowner.IsTimeOut()) { // Nếu như chưa hết thời gian 1 lần tấn công
                        durationPerAttackCountdowner.Countdowning(Time.deltaTime);
                        if (!deltaShotCountdowner.IsTimeOut()) {    // Thời gian delay 1 lần gây dame
                            deltaShotCountdowner.Countdowning(Time.deltaTime);
                            bulletLaser.Beaming(false);
                        }
                        else {
                            bulletLaser.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * CurAttackData.DamagePercent), null);
                            deltaShotCountdowner.StartCountdown(CurAttackData.DeltaShot);
                            bulletLaser.Beaming(true);
                        }
                    }
                    else { // nếu như hết thời gian 1 lần tấn công rồi
                        bulletLaser.EndBeam();
                        bulletLaser.gameObject.SetActive(true);
                    }
                }
            }
            else { // Hết 1 lượt bắn rồi
                durationPerAttackCountdowner.StartCountdown(CurAttackData.DurationPerAttack);
                warningCountdowner.StartCountdown(CurAttackData.WarningTime);
                fireRateCountdowner.StartCountdown(CurAttackData.FireRate);
                warningLaser.EndBeam();
                warningLaser.gameObject.SetActive(true);
                attacking = false;
            }
        }
        else { // hết tổng thời gian skill
            if (!attacking)
                EndAttack();
        }
    }

    public override void StopAttack() {
        base.StopAttack();
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private float totalAttackTime;
        [SerializeField] private float damagePercent;
        [SerializeField] private float durationPerAttack;
        [SerializeField] private float deltaShot;
        [SerializeField] private float fireRate;
        [SerializeField] private float warningTime;

        public float TotalAttackTime {
            get => totalAttackTime;
        }
        public float DamagePercent {
            get => damagePercent;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float DurationPerAttack {
            get => durationPerAttack;
        }
        public float FireRate {
            get => fireRate;
        }
        public float WarningTime {
            get => warningTime;
        }
    }
}
