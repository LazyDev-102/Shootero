using UnityEngine;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using System.Collections;
using Helper;

public class B09Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B09Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
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
    private List<Vector2> posCached;

    private Countdowner delayAttackCountdowner = new Countdowner();

    private AttackData CurAttackData {
        get {
            return attackDatas[CurrentPhaseIndex];
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
        for (int i = 0; i < CurAttackData.BoomCount; i++) {
            var item = GameLoader.SpawnBullet(bulletPrefab, bossAttack.B09Base.transform.position);
            if (item) {
                item.SetCanUpdate(false)
                    .SetTimeLife(CurAttackData.ExplosionTime)
                    .SetHitInfor((int)CurAttackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B09Base);
                item.transform.localPosition = bossAttack.transform.localPosition;
                item.transform.localScale = Vector3.one * scaleBoom;
                item.gameObject.SetActive(false);
                bullets.Add(item);
            }
        }
    }
    private void ShootBullet() {
        if (shotCount >= CurAttackData.BoomCount)
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
        bullets[shotCount].transform.DOMove(pos, CurAttackData.BulletSpeed);
        bullets[shotCount].AddOnDestroy(OnBulletExplosion);
        bullets[shotCount].SetCanUpdate(true);
        shotCount++;
    }
    private IEnumerator Attack() {
        attacking = true;
        yield return Yielder.Wait(delayAttack);
        while (shotCount < CurAttackData.BoomCount) {
            ShootBullet();
            yield return Yielder.Wait(timePerShot);
        }

        yield return Yielder.Wait(CurAttackData.ExplosionTime + CurAttackData.BulletSpeed);
        EndAttack();
    }

    private void OnBulletExplosion(Vector3 position) {
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, position);
        if (newExplosioner) {
            newExplosioner.SetHitInfor((int)(bossAttack.B09Base.B09Stat.Atk.Value * CurAttackData.DamageExplosionPercent), null, bossAttack.B09Base)
                            .SetRadius(CurAttackData.BlastRadius)
                            .Explosioning();
        }
    }
    public override void Updating() {
        /*if(!attacking) */
        bossAttack.B09Base.LookTarget();
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
