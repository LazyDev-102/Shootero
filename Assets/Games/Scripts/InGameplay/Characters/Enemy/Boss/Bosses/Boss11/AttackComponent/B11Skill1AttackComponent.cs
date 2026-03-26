using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Gemmob;

public class B11Skill1AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B11Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpreadBullet bulletParent;
    [SerializeField] private FrontBullet bulletChild;
    [SerializeField] private int numberPreloadParent;
    [SerializeField] private int numberPreloadChild;

    private float timeMove = 20;
    private AttackData attackData;
    private List<FrontBullet> bullets;
    private Countdowner delayCountdowner = new Countdowner();

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (bulletParent) {
            bulletParent.PreloadIngame();
            bulletParent.RegisterPool(numberPreloadParent);
        }
        if (bulletChild) {
            bulletChild.PreloadIngame();
            bulletChild.RegisterPool(numberPreloadChild);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Initialize() {
        base.Initialize();
        bullets = new List<FrontBullet>();
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        atk = (int)(bossAttack.B11Base.B11Stat.Atk.Value * attackData.DamagePercentParent);
    }

    public override void Updating() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            bossAttack.B11Base.LookTarget();
        }
        else {
            var bullet = GameLoader.SpawnBullet(bulletParent, firePoint.position);
            bullet = ChangingParentBullet(bullet);
            bullet.SetData(timeMove / attackData.SpeedBulletParent, SpawnChild);
            bullet.Shoot(bossAttack.Target, null);
            EndAttack();
        }
    }

    private void SpawnChild(Vector3 spawnPos) {
        var per = 360 / attackData.ChildCount;
        var offset = UnityEngine.Random.Range(0, per);
        for (int i = 0; i < attackData.ChildCount; i++) {
            var bClone = GameLoader.SpawnBullet(bulletChild, spawnPos);
            bClone = ChangingBullet(bClone);
            bClone.SetSize(bossAttack.B11Base.B11Stat.Size.Value);
            SetRotation(bClone.transform, offset + per * i);
            bClone.Shoot(attackData.SpeedBulletChild, bClone.transform.up);
            bullets.Add(bClone);
        }
    }
    private void SetRotation(Transform bullet, int zRotation) {
        var temp = bullet.eulerAngles;
        temp.z = zRotation;
        bullet.eulerAngles = temp;
    }
    public override void Attacking() {

    }

    public override void EndAttack() {
        base.EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
    }

    private int atk;
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor((int)(atk * attackData.DamagePercentChild), null, bossAttack.BossBase);
        return bullet;
    }
    public T ChangingParentBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor((int)(atk * attackData.DamagePercentParent), null, bossAttack.BossBase);
        return bullet;
    }
    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercentChild;
        [SerializeField] private float damagePercentParent;
        [SerializeField] private float speedBulletParent;
        [SerializeField] private float speedBulletChild;
        [SerializeField] private int childCount;

        public float DamagePercentParent {
            get => damagePercentParent;
        }
        public float DamagePercentChild {
            get => damagePercentChild;
        }
        public float SpeedBulletParent {
            get => speedBulletParent;
        }
        public float SpeedBulletChild {
            get => speedBulletChild;
        }
        public int ChildCount {
            get => childCount;
        }
    }
}
