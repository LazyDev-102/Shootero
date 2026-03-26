using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB08Attack), typeof(MB08Move), typeof(MB08Health))]
[RequireComponent(typeof(MB08Stat), typeof(MB08Hitbox), typeof(MB08Skill))]
[RequireComponent(typeof(MB08Effect), typeof(MB08StateController))]
public class MB08Base : MinibossBase {
    #region References
    private MB08Attack mb08Attack;
    public MB08Attack MB08Attack {
        get {
            if (mb08Attack == null) {
                mb08Attack = EnemyAttack as MB08Attack;
            }
            return mb08Attack;
        }
    }

    private MB08Move mb08Move;
    public MB08Move MB08Move {
        get {
            if (mb08Move == null) {
                mb08Move = EnemyMove as MB08Move;
            }
            return mb08Move;
        }
    }

    private MB08Health mb08Health;
    public MB08Health MB08Health {
        get {
            if (mb08Health == null) {
                mb08Health = EnemyHealth as MB08Health;
            }
            return mb08Health;
        }
    }

    private MB08Stat mb08Stat;
    public MB08Stat MB08Stat {
        get {
            if (mb08Stat == null) {
                mb08Stat = EnemyStat as MB08Stat;
            }
            return mb08Stat;
        }
    }

    private MB08Hitbox mb08Hitbox;
    public MB08Hitbox MB08Hitbox {
        get {
            if (mb08Hitbox == null) {
                mb08Hitbox = EnemyHitbox as MB08Hitbox;
            }
            return mb08Hitbox;
        }
    }

    private MB08Skill mb08Skill;
    public MB08Skill MB08Skill {
        get {
            if (mb08Skill == null) {
                mb08Skill = EnemySkill as MB08Skill;
            }
            return mb08Skill;
        }
    }
    #endregion

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
}
