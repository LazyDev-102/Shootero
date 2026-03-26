using UnityEngine;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;

public class B06RageAttackComponent : BossSkillAttackComponent {
    [SerializeField] private B06Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float timeWaitToFly = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bulletPrefab;
    [SerializeField] private int turns = 3;
    [SerializeField, Range(0f, 0.3f)] private float minBottomPos = 0.1f;
    [SerializeField, Range(0f, 0.3f)] private float minLeftPos = 0.1f;
    [SerializeField, Range(0.8f, 1f)] private float maxTopPos = 0.9f;
    [SerializeField, Range(0.8f, 1f)] private float maxRightPos = 0.9f;
    [SerializeField] private int numberPreload;

    private List<RotateFrontBullet> bullets;

    private Countdowner attack1Countdowner = new Countdowner();
    private Countdowner delayAttackCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private int attackCount = 0;
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

    public override void PreloadIngame() {
        if (bulletPrefab) {
            bulletPrefab.PreloadIngame();
            bulletPrefab.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        base.Initialize();
        bullets = new List<RotateFrontBullet>();
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
        bullets?.Clear();
        attackCount = 0;
    }

    public override void Attacking() {
        delayAttackCountdowner.StartCountdown(delayAttack);
        attack1Countdowner.StartCountdown(0);
        deltaShotCountdowner.StartCountdown(0);
    }
    private void SpawnDarts() {
        for (int i = 0; i < attackData.DartCount; i++) {
            var item = GameLoader.SpawnBullet(bulletPrefab, transform.position);
            item.transform.position = firePoint.position;
            item.transform.localScale = Vector3.one;
            item.SetHitInfor((int)attackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B06Base);
            item.gameObject.SetActive(true);
            bullets.Add(item);
        }
    }
    private void Attack1() {
        if (bullets != null) {
            foreach (var item in bullets) {
                if (item != null)
                    item.Recycle();
            }
            bullets.Clear();
        }
        if (attackCount >= turns) {
            EndAttack();
            return;
        }
        DOVirtual.DelayedCall(0.5f, () => {
            SpawnDarts();
            for (int i = 0; i < bullets.Count; i++) {
                if (bullets[i] == null)
                    continue;
                RandomBulletPos(bullets[i], i);
            }
            attackCount++;
        });
    }
    private void RandomBulletPos(RotateFrontBullet bullet, int index) {
        var number = attackData.DartCount / 4;
        var div = (int)(index / number);
        var mod = (int)(index % number);
        float offset = 0.8f / number;
        Vector2 direction = Vector2.right;
        Vector2 ranVector2 = Vector2.one * 0.5f;
        switch (div) {
            case 0:
                ranVector2 = new Vector2(Random.Range(0.1f + offset * mod, 0.1f + offset * mod + offset), minBottomPos);
                direction = Vector2.up;
                break;
            case 1:
                ranVector2 = new Vector2(minLeftPos, Random.Range(0.1f + offset * mod, 0.1f + offset * mod + offset));
                direction = Vector2.right;
                break;
            case 2:
                ranVector2 = new Vector2(Random.Range(0.1f + offset * mod, 0.1f + offset * mod + offset), maxTopPos);
                direction = Vector2.up * -1;
                break;
            case 3:
            default:
                ranVector2 = new Vector2(maxRightPos, Random.Range(0.1f + offset * mod, 0.1f + offset * mod + offset));
                direction = Vector2.right * -1;
                break;
        }

        var pos = bossAttack.B06Base.B06Move.GetPointMoveB06(ranVector2);
        bullet.gameObject.SetActive(true);
        bullet.transform.position = pos;
        bullet.ResetSpeed(false);
        DOVirtual.DelayedCall(timeWaitToFly, () => bullet.Shoot(direction, attackData.BulletSpeed));
    }
    public override void Updating() {
        bossAttack.B06Base.LookTarget();
        if (delayAttackCountdowner.IsCountdowning()) {
            delayAttackCountdowner.Countdowning(Time.deltaTime);
        }
        else {
            attack1Countdowner.Countdowning(Time.deltaTime);
            if (attack1Countdowner.IsTimeOut()) {
                Attack1();
                attack1Countdowner.StartCountdown(attackData.Attack1Time);
            }
        }
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

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int dartCount;
        [SerializeField] private float attack1Time;

        public float DamagePercent {
            get => damagePercent;
        }
        public float BulletSpeed {
            get => bulletSpeed;
        }
        public float DartCount {
            get => dartCount;
        }
        public float Attack1Time {
            get => attack1Time;
        }
    }
}
