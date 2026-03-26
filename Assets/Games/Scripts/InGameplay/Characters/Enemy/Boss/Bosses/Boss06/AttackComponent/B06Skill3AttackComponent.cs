using UnityEngine;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using Helper;

public class B06Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B06Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float timeRevoke = 1f;
    [SerializeField, Range(0, 10)] private float distancePerBullet = 1f;
    [SerializeField] private Area rangeSpawnBullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bulletPrefab;
    [SerializeField] private AnimationCurve bulletMoveCurve;
    [SerializeField] private float offsetMove = -5;
    [SerializeField] private int numberPreload;

    private List<RotateFrontBullet> bullets = new List<RotateFrontBullet>();
    private List<Vector2> posCached = new List<Vector2>();
    private int shotCount = 0;
    private bool hasAttack2;

    private Countdowner attack1Countdowner = new Countdowner();
    private Countdowner attack2Countdowner = new Countdowner();
    private Countdowner delayAttackCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
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

    public override void StartAttack() {
        attackData = CurAttackData;
        bullets.Clear();
        posCached.Clear();
        shotCount = 0;
        hasAttack2 = false;
        SpawnDarts();
    }
    public override void Attacking() {
        delayAttackCountdowner.StartCountdown(delayAttack);
        attack1Countdowner.StartCountdown(attackData.Attack1Time);
        attack2Countdowner.StartCountdown(attackData.Attack2Time);
        deltaShotCountdowner.StartCountdown(0);
    }
    private void SpawnDarts() {
        for (int i = 0; i < attackData.DartCount; i++) {
            var item = GameLoader.SpawnBullet(bulletPrefab, bossAttack.B06Base.transform.position);
            if (item) {
                item.transform.localPosition = bossAttack.transform.localPosition;
                item.transform.localScale = Vector3.one * 2;
                item.SetHitInfor((int)attackData.DamagePercent * bossAttack.CharacterBase.CharacterStat.Atk.Value, null, bossAttack.B06Base);
                item.gameObject.SetActive(false);
                bullets.Add(item);
            }
        }
    }
    private void Attack1() {//Move out
        if (shotCount >= attackData.DartCount)
            return;
        var x = Random.Range(0f, 1f);
        var y = shotCount == 0 ? Random.Range(0.35f, 0.5f) : shotCount == 1 ? Random.Range(0.2f, 0.35f) : shotCount == 2 ? Random.Range(0f, 0.25f) : Random.Range(0f, 0.5f);
        var ranVector2 = new Vector2(x, y);
        var pos = bossAttack.B06Base.B06Move.GetPointMoveB06(ranVector2);
        bullets[shotCount].gameObject.SetActive(true);
        bullets[shotCount].transform.DOMove(pos, attackData.BulletSpeed);
        shotCount++;
    }
    private void Attack11() {//Move out
        if (shotCount >= attackData.DartCount || shotCount >= bullets.Count)
            return;
        Vector2 ranVector2 = Vector2.zero, pos = Vector2.zero;
        bool distance = false;
        var times = 20;
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
        var index = shotCount;
        DOVirtual.DelayedCall(attackData.BulletSpeed, () => {
            Vector3 newPos = bullets[index].transform.position + bullets[index].transform.up * offsetMove;// new Vector3(bullets[index].transform.position.x * offset, bullets[index].transform.position.y * offset, bullets[index].transform.position.z * offset);
            bullets[index].transform.DOMove(newPos, 5f).SetEase(bulletMoveCurve);
        });
        bullets[shotCount].gameObject.SetActive(true);
        bullets[shotCount].transform.DOMove(pos, attackData.BulletSpeed).SetEase(bulletMoveCurve);
        shotCount++;
    }
    private void Attack2() {//Move in
        if (hasAttack2)
            return;
        bossAttack.BossBase.BossMove.EndMoveIdle();
        hasAttack2 = true;
        foreach (var item in bullets) {
            item.transform.DOKill();
            item.transform.DOMove(bossAttack.transform.position, timeRevoke).OnComplete(() => {
                item.gameObject.SetActive(false);
                EndAttack();
            });
        }

    }
    public override void Updating() {
        bossAttack.B06Base.LookTarget();
        if (delayAttackCountdowner.IsCountdowning()) {
            delayAttackCountdowner.Countdowning(Time.deltaTime);
        }
        else {
            attack1Countdowner.Countdowning(Time.deltaTime);
            if (attack1Countdowner.IsCountdowning()) {
                deltaShotCountdowner.Countdowning(Time.deltaTime);
                if (deltaShotCountdowner.IsTimeOut()) {
                    //Attack1();
                    Attack11();
                    deltaShotCountdowner.StartCountdown(attackData.DeltaShot);
                }
            }
            else {
                attack2Countdowner.Countdowning(Time.deltaTime);
                if (attack2Countdowner.IsCountdowning()) {
                    Attack2();
                }
            }
        }
    }
    public override void EndAttack() {
        if (bullets == null) {
            return;
        }
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
        bullets.Clear();
        base.EndAttack();
    }
    private void OnDisable() {
        if (bullets == null) {
            return;
        }
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
        bullets.Clear();
    }
    public override void StopAttack() {
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
        bullets.Clear();
        base.StopAttack();
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int dartCount;
        [SerializeField] private float attack1Time;
        [SerializeField] private float attack2Time;

        public float DamagePercent {
            get => damagePercent;
        }
        public float BulletSpeed {
            get => bulletSpeed;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float DartCount {
            get => dartCount;
        }
        public float Attack1Time {
            get => attack1Time;
        }
        public float Attack2Time {
            get => attack2Time;
        }
    }
}
