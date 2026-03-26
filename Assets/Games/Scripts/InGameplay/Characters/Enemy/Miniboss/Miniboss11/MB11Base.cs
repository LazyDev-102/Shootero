using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB11Attack), typeof(MB11Move), typeof(MB11Health))]
[RequireComponent(typeof(MB11Stat), typeof(MB11Hitbox), typeof(MB11Skill))]
[RequireComponent(typeof(MB11Effect), typeof(MB11StateController))]
public class MB11Base : MinibossBase {
    #region References
    private MB11Attack mb11Attack;
    public MB11Attack MB11Attack {
        get {
            if (mb11Attack == null) {
                mb11Attack = EnemyAttack as MB11Attack;
            }
            return mb11Attack;
        }
    }

    private MB11Move mb11Move;
    public MB11Move MB11Move {
        get {
            if (mb11Move == null) {
                mb11Move = EnemyMove as MB11Move;
            }
            return mb11Move;
        }
    }

    private MB11Health mb11Health;
    public MB11Health MB11Health {
        get {
            if (mb11Health == null) {
                mb11Health = EnemyHealth as MB11Health;
            }
            return mb11Health;
        }
    }

    private MB11Stat mb11Stat;
    public MB11Stat MB11Stat {
        get {
            if (mb11Stat == null) {
                mb11Stat = EnemyStat as MB11Stat;
            }
            return mb11Stat;
        }
    }

    private MB11Hitbox mb11Hitbox;
    public MB11Hitbox MB11Hitbox {
        get {
            if (mb11Hitbox == null) {
                mb11Hitbox = EnemyHitbox as MB11Hitbox;
            }
            return mb11Hitbox;
        }
    }

    private MB11Skill mb11Skill;
    public MB11Skill MB11Skill {
        get {
            if (mb11Skill == null) {
                mb11Skill = EnemySkill as MB11Skill;
            }
            return mb11Skill;
        }
    }
    #endregion

    #region Special Attack
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private RangeIntValue limitRange;
    [SerializeField] private float delaySpawn;

    private Vector2 spawnPosition = new Vector2(100, 100);
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
            var spawnPosition = new Vector2(Random.Range(-200, 200), Random.Range(0, 5));
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
