using Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gemmob;

public class B10Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B10Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ME03B10Base me;
    [SerializeField] private Area area;
    [SerializeField] private float delayAfterAttack;
    [SerializeField] private int numberPreload;


    private List<ME03B10Base> minienemies;
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
        if (me) {
            me.PreloadIngame();
            me.RegisterPool(numberPreload);
        }

    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void Initialize() {
        base.Initialize();
        minienemies = new List<ME03B10Base>();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.B10Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        yield return Yielder.Wait(delayAttack);
        for (int i = 0; i < attackData.NumberShot; ++i) {
            ME03B10Base newBigBrother = GameLoader.SpawnEnemy(me, firePoint.position);
            if (newBigBrother) {
                newBigBrother.SetBigBrother(true);
                newBigBrother.SetParentBoss(bossAttack.B10Base);
                newBigBrother.Initialize();
                newBigBrother.EnemyStat.Atk.SetBaseValue((int)(bossAttack.B10Base.EnemyStat.Atk.Value * attackData.DamageMiniEnemyPercent));
                newBigBrother.EnemyStat.MaxHP.SetBaseValue((int)(bossAttack.B10Base.EnemyStat.MaxHP.Value * attackData.HpMiniEnemyPercent));
                newBigBrother.AddOnMEDie(OnMERemove);
            }

            ME03B10Base newME = GameLoader.SpawnEnemy(me, firePoint.position);
            if (newME) {
                newME.SetBigBrother(false);
                newME.SetParentBoss(bossAttack.B10Base);
                newME.Initialize();
                newME.EnemyStat.Atk.SetBaseValue((int)(bossAttack.B10Base.EnemyStat.Atk.Value * attackData.DamageMiniEnemyPercent));
                newME.EnemyStat.MaxHP.SetBaseValue((int)(bossAttack.B10Base.EnemyStat.MaxHP.Value * attackData.HpMiniEnemyPercent));
                newME.AddOnMEDie(OnMERemove);
            }

            newBigBrother.ME03B10Move.SetMovePoint(ChoosePointMove(attackData.MinDistance, attackData.MaxDistance, null));
            newME.ME03B10Move.SetMovePoint(ChoosePointMove(attackData.MinDistance, attackData.MaxDistance, newBigBrother));
            newBigBrother.SetBrother(newME);
            newME.SetBrother(newBigBrother);
            minienemies.Add(newME);
            minienemies.Add(newBigBrother);

            yield return Yielder.Wait(attackData.DeltaShot);
        }
        yield return Yielder.Wait(delayAfterAttack);
        EndAttack();
    }

    public override void BossDestroy() {
        foreach (var me in minienemies) {
            if (me) {
                me.SelfDestruction();
            }
        }
    }

    private void OnMERemove(ME03B10Base me) {
        if (me) {
            minienemies.Remove(me);
        }
    }

    public Vector2 ChoosePointMove(float minDistance, float maxDistance, ME03B10Base me) {
        Vector2 pointMove = Vector2.zero;
        if (me) {
            Vector2 pointBigBrother = me.ME03B10Move.GetMovePoint();
            do {
                float distance = UnityEngine.Random.Range(minDistance, maxDistance);
                pointMove = pointBigBrother + UnityEngine.Random.insideUnitCircle.normalized * distance;
            } while (BorderHelper.IsOutBound(pointMove));
        }
        else {
            pointMove = BorderHelper.GetWorldPointInsideArea(area);
        }
        return pointMove;
    }


    [Serializable]
    private class AttackData {
        [SerializeField] private float damageMiniEnemyPercent;
        [SerializeField] private float hpMiniEnemyPercent;
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;

        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;


        public int NumberShot { get => numberShot; }
        public float DamageMiniEnemyPercent { get => damageMiniEnemyPercent; }
        public float HpMiniEnemyPercent { get => hpMiniEnemyPercent; }
        public float DeltaShot { get => deltaShot; }
        public float MinDistance { get => minDistance; }
        public float MaxDistance { get => maxDistance; }

    }
}
