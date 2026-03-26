using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB13Attack), typeof(MB13Move), typeof(MB13Health))]
[RequireComponent(typeof(MB13Stat), typeof(MB13Hitbox), typeof(MB13Skill))]
[RequireComponent(typeof(MB13Effect), typeof(MB13StateController))]
public class MB13Base : MinibossBase {
    #region References
    private MB13Attack mb13Attack;
    public MB13Attack MB13Attack {
        get {
            if (mb13Attack == null) {
                mb13Attack = EnemyAttack as MB13Attack;
            }
            return mb13Attack;
        }
    }

    private MB13Move mb13Move;
    public MB13Move MB13Move {
        get {
            if (mb13Move == null) {
                mb13Move = EnemyMove as MB13Move;
            }
            return mb13Move;
        }
    }

    private MB13Health mb13Health;
    public MB13Health MB13Health {
        get {
            if (mb13Health == null) {
                mb13Health = EnemyHealth as MB13Health;
            }
            return mb13Health;
        }
    }

    private MB13Stat mb13Stat;
    public MB13Stat MB13Stat {
        get {
            if (mb13Stat == null) {
                mb13Stat = EnemyStat as MB13Stat;
            }
            return mb13Stat;
        }
    }

    private MB13Hitbox mb13Hitbox;
    public MB13Hitbox MB13Hitbox {
        get {
            if (mb13Hitbox == null) {
                mb13Hitbox = EnemyHitbox as MB13Hitbox;
            }
            return mb13Hitbox;
        }
    }

    private MB13Skill mb13Skill;
    public MB13Skill MB13Skill {
        get {
            if (mb13Skill == null) {
                mb13Skill = EnemySkill as MB13Skill;
            }
            return mb13Skill;
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
