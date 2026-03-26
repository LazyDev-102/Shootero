

using System;
using UnityEngine;

public class B04Skill1AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B04Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float warningTime;
    [SerializeField] private BasicLaser warningLine;
    [SerializeField] private BasicLaser laser;
    [SerializeField, Range(0, 1)] private float timeOffLaserPercent = 1;

    bool isBeaming;

    private Countdowner warningCountdowner;
    private Countdowner durantionShotCountdowner;
    private Countdowner deltaShotCountdowner;
    private HitInfor hitInfor;
    private int ishot;
    private float curDurationShot;
    private AttackData attackData;

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

    public override void Initialize() {
        HideWarning();
        isBeaming = false;
        laser.EndBeam();
        laser.gameObject.SetActive(false);
    }

    public override void Attacking() {
        StartBeam();
    }


    private void DrawWarning() {
        warningLine.gameObject.SetActive(true);
        warningLine.Beaming(false);
    }

    private void HideWarning() {
        warningLine.gameObject.SetActive(false);
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        isBeaming = false;
        warningLine.SetPercentSize(1);
        hitInfor = new HitInfor();
        hitInfor.SetInfor((int)(bossAttack.B04Base.B04Stat.Atk.Value * attackData.DamagePercent), null, bossAttack.B04Base);
    }


    public void StartBeam() {
        curDurationShot = attackData.DurantionShot;
        warningCountdowner.StartCountdown(warningTime);
        durantionShotCountdowner.StartCountdown(attackData.DurantionShot);
        deltaShotCountdowner.StartCountdown(attackData.DeltaDamage);
        isBeaming = true;
        ishot = 0;
    }

    public override void Updating() {
        if (isBeaming) {
            if (warningCountdowner.IsTimeOut()) {
                if (durantionShotCountdowner.IsCountdowning()) {
                    if (deltaShotCountdowner.IsTimeOut()) {
                        laser.SetInfor(hitInfor);
                        laser.Beaming(true);
                        deltaShotCountdowner.StartCountdown(attackData.DeltaDamage);
                    }
                    else {
                        deltaShotCountdowner.Countdowning(Time.deltaTime);
                        laser.Beaming(false);
                    }
                    durantionShotCountdowner.Countdowning(Time.deltaTime);
                    float timeOffPoint = curDurationShot * (1 - timeOffLaserPercent);
                    float timeOffElapsed = durantionShotCountdowner.Countdown;
                    if (timeOffElapsed < timeOffPoint) {
                        float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
                        laser.SetPercentSize(percentSize);
                    }
                    if (durantionShotCountdowner.IsTimeOut()) {
                        laser.gameObject.SetActive(false);
                        warningCountdowner.StartCountdown(warningTime);
                        warningLine.StartBeam();
                        ishot++;
                        if (ishot >= attackData.NumberShot) {
                            isBeaming = false;
                            EndAttack();
                        }
                        else {

                        }
                    }
                }
            }
            else {
                bossAttack.B04Base.B04Move.LookTarget(bossAttack.Target.position, lookSpeed);
                DrawWarning();
                warningCountdowner.Countdowning(Time.deltaTime);
                if (warningCountdowner.IsTimeOut()) {
                    durantionShotCountdowner.StartCountdown(attackData.DurantionShot);
                    laser.StartBeam();
                    laser.gameObject.SetActive(true);
                    HideWarning();
                }
            }
        }
    }

    public override void StopAttack() {
        HideWarning();
        laser.EndBeam();
        laser.gameObject.SetActive(false);
        base.StopAttack();
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaDamage;
        [SerializeField] private float damagePercent;
        [SerializeField] private float durantionShot;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaDamage { get => deltaDamage; }
        public float DurantionShot { get => durantionShot; }

    }
}
