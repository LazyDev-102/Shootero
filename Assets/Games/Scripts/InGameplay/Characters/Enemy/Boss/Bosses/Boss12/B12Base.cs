using Gemmob;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(B12Attack), typeof(B12Health), typeof(B12Move))]
[RequireComponent(typeof(B12Skill), typeof(B12Stat), typeof(B12HitBox))]
[RequireComponent(typeof(B12StateController), typeof(B12Effect))]
public class B12Base : BossBase {
    #region References
    private B12Attack b12Attack;
    public B12Attack B12Attack {
        get {
            if (b12Attack == null) {
                b12Attack = BossAttack as B12Attack;
            }
            return b12Attack;
        }
    }

    private B12Move b12Move;
    public B12Move B12Move {
        get {
            if (b12Move == null) {
                b12Move = BossMove as B12Move;
            }
            return b12Move;
        }
    }

    private B12Health b12Health;
    public B12Health B12Health {
        get {
            if (b12Health == null) {
                b12Health = BossHealth as B12Health;
            }
            return b12Health;
        }
    }

    private B12Stat b12Stat;
    public B12Stat B12Stat {
        get {
            if (b12Stat == null) {
                b12Stat = BossStat as B12Stat;
            }
            return b12Stat;
        }
    }

    private B12HitBox b12Hitbox;
    public B12HitBox B12Hitbox {
        get {
            if (b12Hitbox == null) {
                b12Hitbox = BossHitbox as B12HitBox;
            }
            return b12Hitbox;
        }
    }

    private B12Skill b12Skill;
    public B12Skill B12Skill {
        get {
            if (b12Skill == null) {
                b12Skill = BossSkill as B12Skill;
            }
            return b12Skill;
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
    private List<EnemyBase> enemies = new List<EnemyBase>();
    private bool canSpecialAttack;
    public override void Initialize() {
        gameLoader = GameManager.Instance.GameLoader;
        limit = limitRange.GetRandomValue();
        enemies = new List<EnemyBase>();
        curMultiplerValue = GetMultiplerValue();
        //CalculationDelaySpawnEnemy();
        countdownSpawnCountdowner.StartCountdown(delaySpawn);
        canSpecialAttack = true;
        base.Initialize();
    }

    private float GetMultiplerValue() {
        if (GameManager.Instance.isTest) {
            return 1;
        }
        return GameManager.Instance.GameController.GetDifficultMultiple();
    }

    public override void Updating() {
        base.Updating();
        if (canSpecialAttack) {
            countdownSpawnCountdowner.Countdowning(Time.deltaTime);
            if (countdownSpawnCountdowner.IsTimeOut()) {
                SpawnEnemies();
            }
        }
    }

    public override void Destroy() {
        base.Destroy();
        if (enemies != null) {
            foreach (var e in enemies) {
                e.RemoveOnRemove(OnEnemyRemove);
                if (e != null)
                    e.Recycle();
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
            //EnemyBase newEnemy = gameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            EnemyBase newEnemy = enemyPrefab.Spawn(gameLoader.transform, spawnPosition);
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
    public void SetCanSpecialAttack(bool status) {
        canSpecialAttack = status;
    }
    public void ClearEnemyChild() {
        if (enemies != null) {
            foreach (var e in enemies) {
                e.RemoveOnRemove(OnEnemyRemove);
            }
        }
    }
    public override void PreloadIngame() {
        base.PreloadIngame();
        enemyPrefab.RegisterPool(10);
    }
    #endregion
}
