using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B12Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B12Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float startAttackTime = 1.5f;
    [SerializeField] private float timeMove = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private B12ChildBase bulletPrefab;
    [SerializeField] private ParticleSystem effectPrefab;

    private Countdowner delayCountdowner = new Countdowner();
    private AttackData attackData;
    private List<B12ChildBase> bullets;
    private List<ParticleSystem> effects;
    private bool attacking;
    public override void Initialize() {
        bullets = new List<B12ChildBase>();
        effects = new List<ParticleSystem>();
        base.Initialize();
    }
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

    public override void StartAttack() {
        attackData = CurAttackData;
        attacking = false;
        delayCountdowner.StartCountdown(delayAttack);
        bossAttack.B12Base.B12Move.StopMoveIdle();
    }


    public override void Updating() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            bossAttack.B12Base.B12Move.LookTarget(bossAttack.Target.position);
        }
        else {
            if (!attacking) {
                attacking = true;
                SpawnBullet();
            }
        }
    }
    private void SpawnBullet() {
        bullets.Clear();
        effects.Clear();

        var per = 360 / attackData.BulletCount;
        var offset = Random.Range(0, per);
        for (int i = 0; i < attackData.BulletCount; i++) {
            var bClone = bulletPrefab.Spawn(transform, transform.position);
            var eClone = effectPrefab.Spawn(transform, transform.position);
            bClone.gameObject.SetActive(false);
            SetRotation(bClone.transform, offset + per * i);
            SetRotation(eClone.transform, offset + per * i);
            bullets.Add(bClone);
            effects.Add(eClone);
        }

        if (gameObject.activeInHierarchy)
            StartCoroutine(State1());
    }
    private void SetRotation(Transform bullet, int zRotation) {
        var temp = bullet.eulerAngles;
        temp.z = zRotation;
        bullet.eulerAngles = temp;
    }
    private IEnumerator State1() {
        foreach (var item in effects) {
            item.transform.DOMove(item.transform.position + item.transform.up * 5, 0.5f).OnComplete(() => {
                item.Play();
            });
        }
        foreach (var item in bullets) {
            item.transform.DOMove(item.transform.position + item.transform.up * 5, 0.5f).OnComplete(() => {
                SetRotation(item.transform, 0);
                item.gameObject.SetActive(true);
            });
        }
        yield return StartCoroutine(State2());
    }
    private IEnumerator State2() {
        yield return Yielder.Wait(startAttackTime / 2);
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] == null)
                continue;
            bullets[i].transform.position = effects[i].transform.position;
        }
        yield return Yielder.Wait(startAttackTime / 2);
        foreach (var item in bullets) {
            if (item == null)
                continue;
            var direction = (bossAttack.Target.position - item.transform.position).normalized;
            item.B12ChildMove.StartMove(bossAttack.Target.position + direction * 20, 2f, () => item.Recycle());
        }
        yield return Yielder.Wait(2);
        EndAttack();
    }
    public override void Attacking() {
    }

    public override void StopAttack() {
        foreach (var item in effects) {
            if (item != null)
                item.Recycle();
        }
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
        effects.Clear();
        bullets.Clear();
        base.StopAttack();
        bossAttack.B12Base.B12Move.RestartMoveIdle();
        attacking = false;
    }

    public override void EndAttack() {
        foreach (var item in effects) {
            if (item != null)
                item.Recycle();
        }
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
        effects.Clear();
        bullets.Clear();
        base.EndAttack();
        bossAttack.B12Base.B12Move.RestartMoveIdle();
        attacking = false;
    }
    public override void BossDestroy() {
        base.BossDestroy();
        foreach (var item in effects) {
            if (item != null)
                item.Recycle();
        }
        foreach (var item in bullets) {
            if (item != null)
                item.Recycle();
        }
    }
    private void OnDisable() {
        ClearBullet();
    }
    private void OnDestroy() {
        ClearBullet();
    }
    private void ClearBullet() {
        try {
            if (effects != null) {
                foreach (var item in effects) {
                    if (item != null)
                        item.Recycle();
                }
                effects.Clear();
            }

            if (bullets != null) {
                foreach (var item in bullets) {
                    if (item != null)
                        item.Recycle();
                }
                bullets.Clear();
            }
        }
        catch {
            effects.Clear();
            bullets.Clear();
        }
    }
    [System.Serializable]
    private class AttackData {
        [SerializeField] private int bulletCount;
        [SerializeField] private float bulletSpeed;

        public int BulletCount {
            get => bulletCount;
        }
        public float BulletSpeed {
            get => bulletSpeed;
        }
    }
}
