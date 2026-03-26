using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB17Attack), typeof(MB17Move), typeof(MB17Health))]
[RequireComponent(typeof(MB17Stat), typeof(MB17Hitbox), typeof(MB17Skill))]
[RequireComponent(typeof(MB17Effect), typeof(MB17StateController))]
public class MB17Base : MinibossBase {
    #region References
    private MB17Attack mb17Attack;
    public MB17Attack MB17Attack {
        get {
            if (mb17Attack == null) {
                mb17Attack = EnemyAttack as MB17Attack;
            }
            return mb17Attack;
        }
    }

    private MB17Move mb17Move;
    public MB17Move MB17Move {
        get {
            if (mb17Move == null) {
                mb17Move = EnemyMove as MB17Move;
            }
            return mb17Move;
        }
    }

    private MB17Health mb17Health;
    public MB17Health MB17Health {
        get {
            if (mb17Health == null) {
                mb17Health = EnemyHealth as MB17Health;
            }
            return mb17Health;
        }
    }

    private MB17Stat mb17Stat;
    public MB17Stat MB17Stat {
        get {
            if (mb17Stat == null) {
                mb17Stat = EnemyStat as MB17Stat;
            }
            return mb17Stat;
        }
    }

    private MB17Hitbox mb17Hitbox;
    public MB17Hitbox MB17Hitbox {
        get {
            if (mb17Hitbox == null) {
                mb17Hitbox = EnemyHitbox as MB17Hitbox;
            }
            return mb17Hitbox;
        }
    }

    private MB17Skill mb17Skill;
    public MB17Skill MB17Skill {
        get {
            if (mb17Skill == null) {
                mb17Skill = EnemySkill as MB17Skill;
            }
            return mb17Skill;
        }
    }
    #endregion

    #region Special Attack
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private RangeIntValue limitRange;
    [SerializeField] private float delaySpawn;

    private readonly Vector2 spawnPosition = new Vector2(100, 100);
    private int limit;
    private Countdowner countdownSpawnCountdowner;
    private float curMultiplerValue;
    private GameLoader gameLoader;
    private List<EnemyBase> enemies;

    public override void Initialize() {
        base.Initialize();
        gameLoader = GameManager.Instance.GameLoader;
        limit = limitRange.GetRandomValue();
        enemies = new List<EnemyBase>();
        curMultiplerValue = GetMultiplerValue();
        CalculationDelaySpawnEnemy();
    }

    private float GetMultiplerValue() {
        if (GameManager.Instance.isTest) {
            return 1;
        }
        return GameManager.Instance.GameController.GetDifficultMultiple();
    }

    public override void Updating() {
        base.Updating();
        countdownSpawnCountdowner.Countdowning(Time.deltaTime);
        if (countdownSpawnCountdowner.IsTimeOut()) {
            SpawnEnemies();
        }
    }

    public override void Destroy() {
        base.Destroy();
        if (enemies != null) {
            foreach (var e in enemies) {
                e.RemoveOnRemove(OnEnemyRemove);
            }
        }
    }
    public override void Die() {
        base.Die();
        if (IngameData.currentGameMode == GameMode.Infinity)
            GameManager.Instance.GameLoader.DespawnAllEnemy(true);
    }
    public void CalculationDelaySpawnEnemy() {
        float newDelaySpawn = (1.0f * enemies.Count / limit) * delaySpawn;
        if (!countdownSpawnCountdowner.IsTimeOut() && newDelaySpawn < countdownSpawnCountdowner.Countdown) {
            countdownSpawnCountdowner.StartCountdown(newDelaySpawn);
        }
        else if (countdownSpawnCountdowner.IsTimeOut()) {
            countdownSpawnCountdowner.StartCountdown(newDelaySpawn);
        }
    }

    private void SpawnEnemies() {
        int numberEnemyNeedSpawn = limit - enemies.Count;
        for (int i = 0; i < numberEnemyNeedSpawn; ++i) {
            EnemyBase newEnemy = gameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(curMultiplerValue);
                newEnemy.Initialize();
                newEnemy.AddOnRemove(OnEnemyRemove);
                enemies.Add(newEnemy);
            }
        }
    }

    private void OnEnemyRemove(EnemyBase e) {
        CalculationDelaySpawnEnemy();
        enemies.Remove(e);
    }
    public override void PreloadIngame() {
        base.PreloadIngame();
        enemyPrefab.RegisterPool(5);
    }
    #endregion
}
