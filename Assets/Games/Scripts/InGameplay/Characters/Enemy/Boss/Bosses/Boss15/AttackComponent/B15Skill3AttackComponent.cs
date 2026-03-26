using UnityEngine;
using System.Collections;
using System;
using Gemmob;

public class B15Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B15Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private Transform firePoint;
    [SerializeField] private B15Skill3UltraSound[] bullets;
    private int numberShot;
    private bool canAim;
    private bool canAttack;
    private AttackData attackData;
    private int cPhase;
    private Countdowner warningCd = new Countdowner();

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }
    public override void StartAttack() {
        canAttack = true;
        cPhase = CurrentPhaseIndex;
        attackData = CurAttackData;
        warningCd.StartCountdown(attackData.WarningTime);
        for (int i = 0; i < bullets.Length; i++) {
            for (int j = 0; j < bullets[i].Bullet.Length; j++) {
                bullets[i].Waring[j].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < bullets[cPhase].Waring.Length; i++) {
            bullets[cPhase].Waring[i].gameObject.SetActive(true);
        }
    }
    public override void Attacking() {
    }
    public override void Updating() {
        if (canAttack) {
            if (warningCd.IsTimeOut()) {
                canAttack = false;
                for (int i = 0; i < bullets.Length; i++) {
                    for (int j = 0; j < bullets[i].Waring.Length; j++) {
                        bullets[i].Waring[j].gameObject.SetActive(false);
                    }
                }
                if (gameObject.activeInHierarchy)
                    StartCoroutine(IShotting());
            }
            warningCd.Countdowning(Time.deltaTime);
        }
    }
    private IEnumerator IShotting() {
        canAim = true;
        yield return Yielder.Wait(attackData.DelayAttack);
        canAim = false;
        for (int i = 0; i < bullets.Length; i++) {
            for (int j = 0; j < bullets[i].Bullet.Length; j++) {
                bullets[i].Bullet[j].TurnEffect(i == cPhase);
            }
        }
        for (int j = 0; j < bullets[cPhase].Bullet.Length; j++) {
            bullets[cPhase].Bullet[j].TurnEffect(true);
        }
        SetNumberShot();
        SetBulletInfo();
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            if (gameObject.activeInHierarchy) {
                for (int i = 0; i < bullets.Length; i++) {
                    if (i == cPhase)
                        for (int j = 0; j < bullets[i].Bullet.Length; j++) {
                            StartCoroutine(bullets[i].Bullet[j].IShotting());
                        }
                }
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }
    public override void EndAttack() {
        for (int i = 0; i < bullets.Length; i++) {
            for (int j = 0; j < bullets[i].Bullet.Length; j++) {
                bullets[i].Bullet[j].TurnEffect(false);
            }
        }
        canAttack = false;
        base.EndAttack();
    }
    public override void StopAttack() {
        for (int i = 0; i < bullets.Length; i++) {
            for (int j = 0; j < bullets[i].Bullet.Length; j++) {
                bullets[i].Bullet[j].TurnEffect(false);
            }
        }
        for (int i = 0; i < bullets.Length; i++) {
            for (int j = 0; j < bullets[i].Waring.Length; j++) {
                bullets[i].Waring[j].gameObject.SetActive(false);
            }
        }
        canAttack = false;
        base.StopAttack();
    }
    public void Aim() {
        if (!canAim)
            return;
        bossAttack.B15Base.B15Move.LookTarget(bossAttack.Target.transform.position);
    }
    private void SetNumberShot() {
        numberShot = (int)(attackData.DurantionShot / attackData.DeltaShot);
        if (numberShot < 1)
            numberShot = 1;
    }
    private void SetBulletInfo() {
        var b15Stat = bossAttack.B15Base.B15Stat;
        int damage = (int)(b15Stat.Atk.Value * b15Stat.ColliderDamage.Value * attackData.DamagePercent);
        for (int i = 0; i < bullets[cPhase].Bullet.Length; i++) {
            bullets[cPhase].Bullet[i].SetInfo(damage, attackData.DeltaShot, bossAttack.B15Base);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }


    [Serializable]
    public class B15Skill3UltraSound {
        [SerializeField] private B15UltraSound[] bullet;
        [SerializeField] private BasicLaser[] waring;

        public B15UltraSound[] Bullet { get => bullet; }
        public BasicLaser[] Waring { get => waring; }
    }
    [Serializable]
    private class AttackData {
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float durantion;
        [SerializeField] private float delayAttack;
        [SerializeField] private float warningTime;

        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float DurantionShot { get => durantion; }
        public float DelayAttack { get => delayAttack; }
        public float WarningTime { get => warningTime; }

    }
}
