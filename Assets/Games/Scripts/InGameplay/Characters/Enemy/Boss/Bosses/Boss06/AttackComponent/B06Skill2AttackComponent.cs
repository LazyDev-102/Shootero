using UnityEngine;
using System;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class B06Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B06Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float timeRevoke = 1f;
    [SerializeField] private float speedRevoke = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bulletPrefab;
    [SerializeField] private int numberPreload;
    private Transform bulletParent;

    private List<RotateFrontBullet> bullets = new List<RotateFrontBullet>();
    private bool hasAttack1;
    private bool hasAttack2;
    private bool hasAttack3;

    private bool attacking;
    private Countdowner attack1Countdowner = new Countdowner();
    private Countdowner attack2Countdowner = new Countdowner();
    private Countdowner attack3Countdowner = new Countdowner();
    private Countdowner delayAttackCountdowner = new Countdowner();
    private AttackData attackData;


    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (bulletPrefab) {
            bulletPrefab.PreloadIngame();
            bulletPrefab.RegisterPool(numberPreload);
        }

    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Initialize() {
        base.Initialize();
        bulletParent = new GameObject("B06BulletParent").transform;
        bullets = new List<RotateFrontBullet>();
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        hasAttack1 = false;
        hasAttack2 = false;
        hasAttack3 = false;
        attacking = false;
        bullets?.Clear();
        SpawnDarts();
        bossAttack.B06Base.B06Move.StartMoveAfterAttackB06(new Vector2(0.5f, 0.5f));
    }
    public override void Attacking() {
        delayAttackCountdowner.StartCountdown(delayAttack);
        attack1Countdowner.StartCountdown(attackData.Attack1Time);
        attack2Countdowner.StartCountdown(attackData.Attack2Time);
        attack3Countdowner.StartCountdown(attackData.Attack3Time);
    }
    private void SpawnDarts() {
        if (bulletParent == null) {
            bulletParent = new GameObject("B06BulletParent").transform;
        }
        bulletParent.localPosition = Vector3.zero;
        for (int i = 0; i < attackData.DartCount; i++) {
            var item = GameLoader.SpawnBullet(bulletPrefab, transform.position);
            item.Initalize();
            item.gameObject.SetActive(false);
            item.transform.localPosition = firePoint.localPosition;
            item.transform.localScale = Vector3.one * 2;
            item.SetHitInfor((int)attackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B06Base);
            item.transform.SetParent(bulletParent);
            bullets.Add(item);
        }
    }
    private void Attack1() {//Move out
        if (hasAttack1)
            return;
        bulletParent.transform.position = firePoint.position;
        hasAttack1 = true;
        int per = 360 / bullets.Count;
        for (int i = 0; i < bullets.Count; i++) {
            var radians = (Math.PI / 180) * per * i;
            bullets[i].gameObject.SetActive(true);
            bullets[i].transform.DOMove(bulletParent.position + new Vector3((float)Math.Cos(radians), (float)Math.Sin(radians)) * attackData.RadiusRoundSize, attackData.BulletSpeed);
        }
    }
    TweenerCore<Quaternion, Vector3, QuaternionOptions> rotateTween;
    private void Attack2() {//Rotation
        if (hasAttack2)
            return;
        hasAttack2 = true;
        rotateTween = bulletParent.DOLocalRotate(new Vector3(0, 0, -180), attackData.TimeRotation).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
    private void NewAttack3() {//Move in
        if (hasAttack3)
            return;
        bossAttack.BossBase.BossMove.EndMoveIdle();
        hasAttack3 = true;
        if (bulletParent == null)
            bulletParent = new GameObject("B06BulletParent").transform;
        else
            bulletParent.transform.position = bossAttack.transform.position;

        foreach (var item in bullets) {
            if (item != null)
                item.transform.DOLocalMove(bossAttack.transform.position, timeRevoke).OnComplete(() => {
                    item.Recycle();
                });
        }
        DOVirtual.DelayedCall(timeRevoke + 0.1f, EndAttack);

    }
    public override void EndAttack() {
        if (bullets == null) {
            base.EndAttack();
        }
        else {
            foreach (var item in bullets) {
                if (item != null)
                    item.Recycle();
            }
            bullets.Clear();
            base.EndAttack();
        }
    }

    public override void Updating() {
        if (!attacking) {
            attacking = bossAttack.B06Base.B06Move.CompleteMoveToTarget();
        }
        else {
            bossAttack.B06Base.LookTarget();
            if (delayAttackCountdowner.IsCountdowning()) {
                delayAttackCountdowner.Countdowning(Time.deltaTime);
            }
            else {
                //Attack
                attack1Countdowner.Countdowning(Time.deltaTime);
                if (attack1Countdowner.IsCountdowning()) {
                    Attack1();
                }
                else {
                    attack2Countdowner.Countdowning(Time.deltaTime);
                    if (attack2Countdowner.IsCountdowning()) {
                        Attack2();
                    }
                    else {
                        //Attack3();
                        NewAttack3();
                    }
                }
            }
        }
    }


    public override void StopAttack() {
        if (bullets == null) {
            base.StopAttack();
        }
        else {
            foreach (var item in bullets) {
                if (item != null)
                    item.Recycle();
            }
            bullets.Clear();
            base.StopAttack();
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField, Tooltip("Size vòng tròn tỏa ra: 3->10")] private float radiusRoundSize;
        [SerializeField, Tooltip("Value small == Speed up")] private float bulletSpeed;
        [SerializeField] private int dartCount;
        [SerializeField, Tooltip("Value small == Speed up")] private float timeRotation;
        [SerializeField] private float attack1Time;
        [SerializeField] private float attack2Time;
        [SerializeField] private float attack3Time;

        public float DamagePercent {
            get => damagePercent;
        }
        public float BulletSpeed {
            get => bulletSpeed;
        }
        public float RadiusRoundSize {
            get => radiusRoundSize;
        }
        public float DartCount {
            get => dartCount;
        }
        public float TimeRotation {
            get => timeRotation;
        }
        public float Attack1Time {
            get => attack1Time;
        }
        public float Attack2Time {
            get => attack2Time;
        }
        public float Attack3Time {
            get => attack3Time;
        }
    }
}
