using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB16Attack), typeof(MB16Move), typeof(MB16Health))]
[RequireComponent(typeof(MB16Stat), typeof(MB16Hitbox), typeof(MB16Skill))]
[RequireComponent(typeof(MB16Effect), typeof(MB16StateController))]
public class MB16Base : MinibossBase {
    #region References
    private MB16Attack mb16Attack;
    public MB16Attack MB16Attack {
        get {
            if (mb16Attack == null) {
                mb16Attack = EnemyAttack as MB16Attack;
            }
            return mb16Attack;
        }
    }

    private MB16Move mb16Move;
    public MB16Move MB16Move {
        get {
            if (mb16Move == null) {
                mb16Move = EnemyMove as MB16Move;
            }
            return mb16Move;
        }
    }

    private MB16Health mb16Health;
    public MB16Health MB16Health {
        get {
            if (mb16Health == null) {
                mb16Health = EnemyHealth as MB16Health;
            }
            return mb16Health;
        }
    }

    private MB16Stat mb16Stat;
    public MB16Stat MB16Stat {
        get {
            if (mb16Stat == null) {
                mb16Stat = EnemyStat as MB16Stat;
            }
            return mb16Stat;
        }
    }

    private MB16Hitbox mb16Hitbox;
    public MB16Hitbox MB16Hitbox {
        get {
            if (mb16Hitbox == null) {
                mb16Hitbox = EnemyHitbox as MB16Hitbox;
            }
            return mb16Hitbox;
        }
    }

    private MB16Skill mb16Skill;
    public MB16Skill MB16Skill {
        get {
            if (mb16Skill == null) {
                mb16Skill = EnemySkill as MB16Skill;
            }
            return mb16Skill;
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
