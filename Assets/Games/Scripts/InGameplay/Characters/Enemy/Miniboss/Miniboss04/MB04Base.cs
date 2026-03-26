using Gemmob;
using System.Collections.Generic;
using UnityEngine;

public class MB04Base : MinibossBase {
    #region References
    private MB04Attack mb04Attack;
    public MB04Attack MB04Attack {
        get {
            if (mb04Attack == null) {
                mb04Attack = EnemyAttack as MB04Attack;
            }
            return mb04Attack;
        }
    }

    private MB04Move mb04Move;
    public MB04Move MB04Move {
        get {
            if (mb04Move == null) {
                mb04Move = EnemyMove as MB04Move;
            }
            return mb04Move;
        }
    }

    private MB04Health mb04Health;
    public MB04Health MB04Health {
        get {
            if (mb04Health == null) {
                mb04Health = EnemyHealth as MB04Health;
            }
            return mb04Health;
        }
    }

    private MB04Stat mb04Stat;
    public MB04Stat MB04Stat {
        get {
            if (mb04Stat == null) {
                mb04Stat = EnemyStat as MB04Stat;
            }
            return mb04Stat;
        }
    }

    private MB04Hitbox mb04Hitbox;
    public MB04Hitbox MB04Hitbox {
        get {
            if (mb04Hitbox == null) {
                mb04Hitbox = EnemyHitbox as MB04Hitbox;
            }
            return mb04Hitbox;
        }
    }

    private MB04Skill mb04Skill;
    public MB04Skill MB04Skill {
        get {
            if (mb04Skill == null) {
                mb04Skill = EnemySkill as MB04Skill;
            }
            return mb04Skill;
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
