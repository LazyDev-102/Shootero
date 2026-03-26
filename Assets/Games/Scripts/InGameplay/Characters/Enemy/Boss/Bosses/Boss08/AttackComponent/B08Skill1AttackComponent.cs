

using Gemmob;
using System;
using UnityEngine;

public class B08Skill1AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B08Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private ME01Base miniEnemyPrefab;
    [SerializeField] private int numberPreload;


    private bool isEnded;
    private int enemyInited;
    private ME01Base newME;

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
        if (miniEnemyPrefab) {
            miniEnemyPrefab.PreloadIngame();
            miniEnemyPrefab.RegisterPool(numberPreload);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        isEnded = false;
        enemyInited = 0;
    }

    public override void Updating() {
        bossAttack.B08Base.LookTarget();
    }
    public override void Attacking() {
        int hp = (int)(attackData.HpPercent * bossAttack.BossBase.BossStat.MaxHP.Value);
        int atk = (int)(attackData.DamagePercent * bossAttack.BossBase.BossStat.Atk.Value);

        newME = miniEnemyPrefab.Spawn(new Vector2(100, 100));
        newME.SetInfo(hp, atk);
        newME.Initialize();
        newME.AddOnEndBossAttack(EnemyComplete);
        enemyInited++;
    }

    private void EnemyComplete() {
        enemyInited--;
        if (enemyInited <= 0) {
            EndAttack();
        }
    }

    public override void EndAttack() {
        if (isEnded) {
            return;
        }
        isEnded = true;
        base.EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
        isEnded = true;
        if (newME) {
            newME.SelfDestruction();
        }
    }
    public override void BossDestroy() {
        base.BossDestroy();
        if (newME) {
            newME.SelfDestruction();
        }
    }


    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float radiateTime;
        [SerializeField] private float hpPercent;

        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float RadiateTime { get => radiateTime; }
        public float HpPercent { get => hpPercent; }
    }
}
