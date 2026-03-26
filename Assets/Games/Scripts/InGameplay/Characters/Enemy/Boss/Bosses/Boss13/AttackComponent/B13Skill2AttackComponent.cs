using UnityEngine;
using DG.Tweening;
using Helper;
using System.Collections.Generic;
using Gemmob;
using System.Collections;

public class B13Skill2AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B13Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float scaleBoom = 2f;
    [SerializeField] private float timePerShot = 1f;
    [SerializeField] private Transform fireFirePoint;
    [SerializeField] private Transform iceFirePoint;
    [SerializeField] private AutoExplosionBullet bulletPrefab;
    [SerializeField] private AutoExplosionBullet bulletIcePrefab;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private Explosioner iceExplosioner;
    [SerializeField] private Area rangeSpawnBullet;
    [SerializeField] private Area rangeSpawnIceBullet;
    [SerializeField, Range(0, 10)] private float distancePerBullet = 1f;
    [SerializeField] private int numberPreloadBullet;
    [SerializeField] private int numberPreloadExplosioner;



    private List<AutoExplosionBullet> bullets = new List<AutoExplosionBullet>();
    private List<AutoExplosionBullet> iceBullets = new List<AutoExplosionBullet>();
    private int shotCount;
    private int iceShotCount;
    private List<Vector2> posCached = new List<Vector2>();
    private AttackData attackData;

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
        if (bulletIcePrefab) {
            bulletIcePrefab.PreloadIngame();
            bulletIcePrefab.RegisterPool(numberPreloadBullet);
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
        bullets.Clear();
        iceBullets.Clear();
        posCached.Clear();
        SpawnBooms();
        shotCount = 0;
        iceShotCount = 0;
        delayAttackCountdowner.StartCountdown(delayAttack);
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Attack());
    }
    private void SpawnBooms() {
        for (int i = 0; i < attackData.BoomCount; i++) {
            var item = GameLoader.SpawnBullet(bulletPrefab, fireFirePoint.position);
            var iceBullet = GameLoader.SpawnBullet(bulletIcePrefab, iceFirePoint.position);
            if (item) {
                item.SetCanUpdate(false)
                    .SetTimeLife(attackData.ExplosionTime)
                    .SetHitInfor((int)attackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B13Base);
                item.transform.localPosition = bossAttack.transform.localPosition;
                item.transform.localScale = Vector3.one * scaleBoom;
                item.gameObject.SetActive(false);
                bullets.Add(item);
            }
            if (iceBullet) {
                iceBullet.SetCanUpdate(false)
                    .SetTimeLife(attackData.ExplosionTime)
                    .SetHitInfor((int)attackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B13Base);
                iceBullet.transform.localPosition = bossAttack.transform.localPosition;
                iceBullet.transform.localScale = Vector3.one * scaleBoom;
                iceBullet.gameObject.SetActive(false);
                iceBullets.Add(iceBullet);
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
        bullets[shotCount].transform.position = fireFirePoint.position;
        bullets[shotCount].transform.DOMove(pos, attackData.BulletSpeed / 10f);
        bullets[shotCount].AddOnDestroy(OnBulletExplosion);
        bullets[shotCount].SetCanUpdate(true);
        shotCount++;
    }
    private void ShootIceBullet() {
        if (iceShotCount >= attackData.BoomCount)
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

            pos = BorderHelper.GetWorldPointInsideArea(rangeSpawnIceBullet);
            if (iceShotCount == 0) {
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
        iceBullets[iceShotCount].gameObject.SetActive(true);
        iceBullets[iceShotCount].transform.position = iceFirePoint.position;
        iceBullets[iceShotCount].transform.DOMove(pos, attackData.BulletSpeed / 10f);
        iceBullets[iceShotCount].AddOnDestroy(OnIceBulletExplosion);
        iceBullets[iceShotCount].SetCanUpdate(true);
        iceShotCount++;
    }
    private IEnumerator Attack() {
        yield return Yielder.Wait(delayAttack);
        while (shotCount < attackData.BoomCount) {
            ShootBullet();
            ShootIceBullet();
            yield return Yielder.Wait(timePerShot);
        }

        yield return Yielder.Wait(attackData.ExplosionTime);
        EndAttack();
    }

    private void OnBulletExplosion(Vector3 position) {
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, position);
        if (newExplosioner) {
            newExplosioner.SetHitInfor((int)(bossAttack.B13Base.B13Stat.Atk.Value * attackData.DamageExplosionPercent), null, bossAttack.B13Base)
                            .SetRadius(attackData.BlastRadius)
                            .Explosioning();
        }
    }
    private void OnIceBulletExplosion(Vector3 position) {
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(iceExplosioner, position);
        if (newExplosioner) {
            newExplosioner.SetHitInfor((int)(bossAttack.B13Base.B13Stat.Atk.Value * attackData.DamageExplosionPercent), null, bossAttack.B13Base)
                            .SetRadius(attackData.BlastRadius)
                            .Explosioning();
        }
    }
    public override void Updating() {
        /*if(!attacking) */
        bossAttack.B13Base.LookTarget();
    }
    private void EndAttackState() {
        if (bullets != null) {
            foreach (var item in bullets) {
                if (item != null)
                    item.Recycle();
            }
            bullets.Clear();
        }
    }

    public override void EndAttack() {
        EndAttackState();
        base.EndAttack();
    }
    public override void StopAttack() {
        EndAttackState();
        base.StopAttack();
    }
    public override void BossDestroy() {
        EndAttackState();
        base.BossDestroy();

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