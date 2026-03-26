using UnityEngine;
using System;

public class B15Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B15Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float warningTime;
    [SerializeField] private BasicLaser leftWarningLine;
    [SerializeField] private BasicLaser leftLaser;
    [SerializeField] private BasicLaser rightWarningLine;
    [SerializeField] private BasicLaser rightLaser;
    [SerializeField] private Transform leftFirePoint;
    [SerializeField] private Transform rightFirePoint;
    [SerializeField] private ParticleSystem leftEffect;
    [SerializeField] private ParticleSystem rightEffect;
    [Range(0, 1)]
    [SerializeField] private float timeOffLaserPercent = 1;
    [SerializeField] private float delayFocusValue;

    bool isBeaming;

    private Countdowner warningCountdowner = new Countdowner();
    private Countdowner durantionShotCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner delayFocus = new Countdowner();
    private HitInfor hitInfor;
    private int ishot;
    private float curDurationShot;
    private bool lockDirection;
    private Vector2 directionLeft = Vector2.zero;
    private Vector2 directionRight = Vector2.zero;
    private Transform target;
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
        leftLaser.EndBeam();
        rightLaser.EndBeam();
        leftLaser.gameObject.SetActive(false);
        rightLaser.gameObject.SetActive(false);
        target = bossAttack.Target;
    }

    public override void Attacking() {
        StartBeam();
        PlayEffect(true);
    }

    private void PlayEffect(bool status) {
        leftEffect.gameObject.SetActive(status);
        rightEffect.gameObject.SetActive(status);
        if (status) {
            leftEffect.Play();
            rightEffect.Play();
        }
    }

    private void DrawWarning() {
        leftWarningLine.gameObject.SetActive(true);
        rightWarningLine.gameObject.SetActive(true);
        leftWarningLine.Beaming(false, directionLeft);
        rightWarningLine.Beaming(false, directionRight);
    }

    private void HideWarning() {
        leftWarningLine.gameObject.SetActive(false);
        rightWarningLine.gameObject.SetActive(false);
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        isBeaming = false;
        lockDirection = false;
        leftWarningLine.transform.position = leftFirePoint.position;
        rightFirePoint.transform.position = rightFirePoint.position;
        leftLaser.transform.position = leftFirePoint.position;
        rightLaser.transform.position = rightFirePoint.position;
        leftWarningLine.SetPercentSize(1);
        rightWarningLine.SetPercentSize(1);
        hitInfor = new HitInfor();
        hitInfor.SetInfor((int)(bossAttack.B15Base.B15Stat.Atk.Value * attackData.DamagePercent), null, bossAttack.B15Base);
    }


    public void StartBeam() {
        curDurationShot = attackData.DurantionShot;
        warningCountdowner.StartCountdown(warningTime);
        durantionShotCountdowner.StartCountdown(attackData.DurantionShot);
        deltaShotCountdowner.StartCountdown(attackData.DeltaDamage);

        isBeaming = true;
        ishot = 0;
    }
    private void UpdateLaserAttack() {
        if (deltaShotCountdowner.IsTimeOut()) {
            leftLaser.SetInfor(hitInfor);
            leftLaser.Beaming(true, directionLeft);
            rightLaser.SetInfor(hitInfor);
            rightLaser.Beaming(true, directionRight);
            deltaShotCountdowner.StartCountdown(attackData.DeltaDamage);
        }
        else {
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            leftLaser.Beaming(false, directionLeft);
            rightLaser.Beaming(false, directionRight);
        }
    }
    private void UpdateLaserSize() {
        float timeOffPoint = curDurationShot * (1 - timeOffLaserPercent);
        float timeOffElapsed = durantionShotCountdowner.Countdown;
        if (timeOffElapsed < timeOffPoint) {
            float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
            leftLaser.SetPercentSize(percentSize);
            rightLaser.SetPercentSize(percentSize);
        }
    }
    private void UpdateDurationAttack() {
        durantionShotCountdowner.Countdowning(Time.deltaTime);
        if (durantionShotCountdowner.IsTimeOut()) {
            leftLaser.gameObject.SetActive(false);
            rightLaser.gameObject.SetActive(false);
            warningCountdowner.StartCountdown(warningTime);
            leftWarningLine.StartBeam();
            rightWarningLine.StartBeam();
            ishot++;
            if (ishot >= attackData.NumberShot) {
                isBeaming = false;
                EndAttack();
            }
            else {

            }
        }
    }
    private void UpdateWarningLaser() {
        warningCountdowner.Countdowning(Time.deltaTime);
        if (warningCountdowner.IsTimeOut()) {
            durantionShotCountdowner.StartCountdown(attackData.DurantionShot);
            leftLaser.StartBeam();
            rightLaser.StartBeam();
            leftLaser.gameObject.SetActive(true);
            rightLaser.gameObject.SetActive(true);
            HideWarning();
        }

    }
    private void GetDirection() {
        if (lockDirection)
            return;
        delayFocus.Countdowning(Time.deltaTime);
        if (delayFocus.IsTimeOut()) {
            directionLeft = (target.position - leftFirePoint.position).normalized;
            directionRight = (target.position - rightFirePoint.position).normalized;
            delayFocus.StartCountdown(delayFocusValue);
        }
    }
    public override void Updating() {
        if (isBeaming) {
            if (warningCountdowner.IsTimeOut()) {
                lockDirection = true;
                if (durantionShotCountdowner.IsCountdowning()) {
                    UpdateLaserAttack();
                    UpdateLaserSize();
                    UpdateDurationAttack();
                }
            }
            else {
                lockDirection = false;
                DrawWarning();
                UpdateWarningLaser();
            }
            GetDirection();
        }
    }
    public virtual void LookDirection(Rigidbody2D rib, Vector2 direction) {
        rib.MoveRotation(Mathf.LerpAngle(rib.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * 5));
    }

    private void CompleteAttack() {
        HideWarning();
        leftLaser.EndBeam();
        leftLaser.gameObject.SetActive(false);
        rightLaser.EndBeam();
        rightLaser.gameObject.SetActive(false);
        PlayEffect(false);
    }

    public override void StopAttack() {
        CompleteAttack();
        base.StopAttack();
    }
    public override void EndAttack() {
        CompleteAttack();
        base.EndAttack();
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
