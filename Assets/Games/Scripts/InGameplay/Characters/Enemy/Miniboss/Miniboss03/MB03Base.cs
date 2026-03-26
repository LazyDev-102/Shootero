using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB03Attack), typeof(MB03Move), typeof(MB03Health))]
[RequireComponent(typeof(MB03Stat), typeof(MB03Hitbox), typeof(MB03Skill))]
[RequireComponent(typeof(MB03Effect), typeof(MB03StateController))]
public class MB03Base : MinibossBase {
    #region References
    private MB03Attack mb03Attack;
    public MB03Attack MB03Attack {
        get {
            if (mb03Attack == null) {
                mb03Attack = EnemyAttack as MB03Attack;
            }
            return mb03Attack;
        }
    }

    private MB03Move mb03Move;
    public MB03Move MB03Move {
        get {
            if (mb03Move == null) {
                mb03Move = EnemyMove as MB03Move;
            }
            return mb03Move;
        }
    }

    private MB03Health mb03Health;
    public MB03Health MB03Health {
        get {
            if (mb03Health == null) {
                mb03Health = EnemyHealth as MB03Health;
            }
            return mb03Health;
        }
    }

    private MB03Stat mb03Stat;
    public MB03Stat MB03Stat {
        get {
            if (mb03Stat == null) {
                mb03Stat = EnemyStat as MB03Stat;
            }
            return mb03Stat;
        }
    }

    private MB03Hitbox mb03Hitbox;
    public MB03Hitbox MB03Hitbox {
        get {
            if (mb03Hitbox == null) {
                mb03Hitbox = EnemyHitbox as MB03Hitbox;
            }
            return mb03Hitbox;
        }
    }

    private MB03Skill mb03Skill;
    public MB03Skill MB03Skill {
        get {
            if (mb03Skill == null) {
                mb03Skill = EnemySkill as MB03Skill;
            }
            return mb03Skill;
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
