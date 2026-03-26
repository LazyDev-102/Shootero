
using UnityEngine;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using System.Collections;
using Helper;

public class B09RefectorSkill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B09RefectorAttack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float scaleBoom = 2f;
    [SerializeField] private float timePerShot = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AutoExplosionBullet bulletPrefab;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private Area rangeSpawnBullet;
    [SerializeField, Range(0, 10)] private float distancePerBullet = 1f;
    [SerializeField] private int numberPreloadBullet;
    [SerializeField] private int numberPreloadExplosioner;



    private List<AutoExplosionBullet> bullets;
    private int shotCount;
    private bool attacking;
    private AttackData attackData;
    private List<Vector2> posCached;

    private Countdowner delayAttackCountdowner = new Countdowner();

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
            bulletPrefab.RegisterPool(numberPreloadBullet);
        }
        if (explosioner) {
            explosioner.PreloadIngame();
            explosioner.RegisterPool(numberPreloadExplosioner);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        bullets?.Clear();
        posCached?.Clear();
        posCached = new List<Vector2>();
        SpawnBooms();
        shotCount = 0;
        attacking = false;
        delayAttackCountdowner.StartCountdown(delayAttack);
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Attack());
    }
    private void SpawnBooms() {
        bullets = new List<AutoExplosionBullet>();
        for (int i = 0; i < attackData.BoomCount; i++) {
            var item = GameLoader.SpawnBullet(bulletPrefab, bossAttack.B09RefectorBase.transform.position);
            if (item) {
                item.SetCanUpdate(false)
                    .SetTimeLife(attackData.ExplosionTime)
                    .SetHitInfor((int)attackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B09RefectorBase);
                item.transform.localPosition = bossAttack.transform.localPosition;
                item.transform.localScale = Vector3.one * scaleBoom;
                item.gameObject.SetActive(false);
                bullets.Add(item);
            }
        }
    }
    private void ShootBullet() {
        if (shotCount >= attackData.BoomCount)
            return;
        Vector2 ranVector2 = Vector2.zero, pos = Vector2.zero;
        bool distance = false;
        var times = 100;
        do {
            times--;
            if (times < 0) {
                Logs.LogError("Break");
                break;
            }
            distance = true;

            pos = BorderHelper.GetWorldPointInsideArea(rangeSpawnBullet);
            if (shotCount == 0) {
                posCached.Add(pos);
                break;
            }
            foreach (var item in posCached) {
                if (Vector2.Distance(pos, item) < distancePerBullet)
                    distance = false;
            }
            if (distance)
                posCached.Add(pos);
        }
        while (distance == false);
        bullets[shotCount].gameObject.SetActive(true);
        bullets[shotCount].transform.DOMove(pos, attackData.BulletSpeed);
        bullets[shotCount].AddOnDestroy(OnBulletExplosion);
        bullets[shotCount].SetCanUpdate(true);
        shotCount++;
    }
    private IEnumerator Attack() {
        attacking = true;
        yield return Yielder.Wait(delayAttack);
        while (shotCount < attackData.BoomCount) {
            ShootBullet();
            yield return Yielder.Wait(timePerShot);
        }

        yield return Yielder.Wait(attackData.ExplosionTime + attackData.BulletSpeed);
        EndAttack();
    }

    private void OnBulletExplosion(Vector3 position) {
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, position);
        if (newExplosioner) {
            newExplosioner.SetHitInfor((int)(bossAttack.B09RefectorBase.B09RefectorStat.Atk.Value * attackData.DamageExplosionPercent), null, bossAttack.B09RefectorBase)
                            .SetRadius(attackData.BlastRadius)
                            .Explosioning();
        }
    }
    public override void Updating() {
        /*if(!attacking) */
        bossAttack.B09RefectorBase.LookTarget();
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
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int boomCount;
        [SerializeField] private float explosionTime;
        [SerializeField] private float blastRadius;
        [SerializeField] private float damageExplosionPercent;

        public float DamagePercent {
            get => damagePercent;
        }
        public float BulletSpeed {
            get => bulletSpeed;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float BoomCount {
            get => boomCount;
        }
        public float ExplosionTime {
            get => explosionTime;
        }
        public float BlastRadius {
            get => blastRadius;
        }
        public float DamageExplosionPercent { get => damageExplosionPercent; }
    }
}
