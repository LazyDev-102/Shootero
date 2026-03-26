using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class B08RageAttackComponent : BossAttackComponent {
    [SerializeField] private B08Attack bossAttack;
    [SerializeField] private MERageB08Base miniEnemyPrefab;
    [SerializeField] private int numberEnemy;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private float radius;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;


    private List<MERageB08Base> enemies = new List<MERageB08Base>();

    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B08Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B08Base.CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (miniEnemyPrefab) {
            miniEnemyPrefab.PreloadIngame();
            miniEnemyPrefab.RegisterPool(numberEnemy);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void EndAttack() {
        base.EndAttack();
        bossAttack.B08Base.B08Hitbox.TurnOffInvulnerable();
        foreach (var e in enemies) {
            e.Recycle();
        }
        enemies.Clear();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        int hp = (int)(attackData.HpPercent * bossAttack.BossBase.BossStat.MaxHP.Value);
        for (int i = enemies.Count; i < numberEnemy; i++) {
            MERageB08Base newEnemy = miniEnemyPrefab.Spawn(enemyContainer);
            enemies.Add(newEnemy);
        }

        float deltaAngle = 360f / numberEnemy;

        for (int i = 0; i < numberEnemy; ++i) {
            enemies[i].SetInfo(hp, attackData.HealPercent);
            enemies[i].MERageB08Attack.SetTargetEnemy(bossAttack.B08Base);
            enemies[i].Initialize();
            float curAngle = i * deltaAngle;
            float x = radius * Mathf.Cos(curAngle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(curAngle * Mathf.Deg2Rad);
            enemies[i].transform.localPosition = new Vector3(x, y, 0);
            enemies[i].transform.RotateLocalEuler(90 + curAngle);
            enemies[i].AddOnMEDead(OnEnemyDead);
        }
    }

    public override void Attacking() {
        bossAttack.B08Base.B08Hitbox.TurnOnInvulnerable(-1);
    }

    public override void Updating() {
        bossAttack.B08Base.B08Move.LookDirection(UnityHelper.Down);
        enemyContainer.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
    }

    private void OnEnemyDead(MERageB08Base e) {
        enemies.Remove(e);
        if (enemies.Count == 0) {
            EndAttack();
        }
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float hpPercent;
        [SerializeField] private float healPercent;

        public float HpPercent { get => hpPercent; }
        public float HealPercent { get => healPercent; }
    }
}
