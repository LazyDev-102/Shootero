

using Gemmob;
using Helper;
using System;
using UnityEngine;

public class B08Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B08Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private ME03B08Base miniEnemyPrefab;
    [SerializeField] private Area leftArea;
    [SerializeField] private Area rightArea;
    [SerializeField] private int numberPreload;
    private bool isEnded;
    private int enemyInited;
    ME03B08Base meLeft;
    ME03B08Base meRight;
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
        if (miniEnemyPrefab) {
            miniEnemyPrefab.PreloadIngame();
            miniEnemyPrefab.RegisterPool(numberPreload);
        }
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

        ME03B08Base newMELeft = miniEnemyPrefab.Spawn(transform.position);
        newMELeft.SetInfo(hp, atk);
        newMELeft.ME03B08Attack.SetShotDuration(attackData.Duration);
        newMELeft.ME03B08Move.SetTargetPosition(BorderHelper.GetWorldPointInsideArea(leftArea));
        newMELeft.Initialize();
        newMELeft.AddOnEndBossAttack(EnemyComplete);
        enemyInited++;

        ME03B08Base newMERight = miniEnemyPrefab.Spawn(transform.position);
        newMERight.SetInfo(hp, atk);
        newMERight.ME03B08Attack.SetShotDuration(attackData.Duration);
        newMERight.ME03B08Move.SetTargetPosition(BorderHelper.GetWorldPointInsideArea(rightArea));
        newMERight.Initialize();
        newMERight.AddOnEndBossAttack(EnemyComplete);
        enemyInited++;

        meLeft = newMELeft;
        meRight = newMERight;
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
        if (meLeft) {
            meLeft.SelfDestruction();
        }
        if (meRight) {
            meRight.SelfDestruction();
        }
    }
    public override void BossDestroy() {
        base.BossDestroy();
        if (meLeft) {
            meLeft.SelfDestruction();
        }
        if (meRight) {
            meRight.SelfDestruction();
        }
    }
    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float duration;
        [SerializeField] private float hpPercent;

        public float DamagePercent { get => damagePercent; }
        public float Duration { get => duration; }
        public float HpPercent { get => hpPercent; }
    }
}


