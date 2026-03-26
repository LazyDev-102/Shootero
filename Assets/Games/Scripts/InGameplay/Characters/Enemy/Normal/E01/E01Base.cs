using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(E01Attack))]
[RequireComponent(typeof(E01Move))]
[RequireComponent(typeof(E01Health))]
[RequireComponent(typeof(E01Stat))]
[RequireComponent(typeof(E01Hitbox))]
[RequireComponent(typeof(E01Skill))]
[RequireComponent(typeof(E01Skin))]
[RequireComponent(typeof(E01StateController))]


public class E01Base : EnemyBase {
    #region References
    private E01Attack e01Attack;
    public E01Attack E01Attack {
        get {
            if (e01Attack == null) {
                e01Attack = EnemyAttack as E01Attack;
            }
            return e01Attack;
        }
    }

    private E01Move e01Move;
    public E01Move E01Move {
        get {
            if (e01Move == null) {
                e01Move = EnemyMove as E01Move;
            }
            return e01Move;
        }
    }

    private E01Health e01Health;
    public E01Health E01Health {
        get {
            if (e01Health == null) {
                e01Health = EnemyHealth as E01Health;
            }
            return e01Health;
        }
    }

    private E01Stat e01Stat;
    public E01Stat E01Stat {
        get {
            if (e01Stat == null) {
                e01Stat = EnemyStat as E01Stat;
            }
            return e01Stat;
        }
    }

    private E01Hitbox e01Hitbox;
    public E01Hitbox E01Hitbox {
        get {
            if (e01Hitbox == null) {
                e01Hitbox = EnemyHitbox as E01Hitbox;
            }
            return e01Hitbox;
        }
    }

    private E01Skill e01Skill;
    public E01Skill E01Skill {
        get {
            if (e01Skill == null) {
                e01Skill = EnemySkill as E01Skill;
            }
            return e01Skill;
        }
    }

    private E01Skin e01Skin;
    public E01Skin E01Skin {
        get {
            if (e01Skin == null) {
                e01Skin = GetComponent<E01Skin>();
            }
            return e01Skin;
        }
    }
    #endregion

    [SerializeField] private E01Base enemySpawnAfterDead;
    [SerializeField] private int numberEnemySpawn;
    [SerializeField] private bool isForceSpawn;

    public override void PreloadIngame() {
        base.PreloadIngame();
        if (enemySpawnAfterDead) {
            for (int i = 0; i < numberEnemySpawn; i++) {
                enemySpawnAfterDead.PreloadIngame();
                enemySpawnAfterDead.RegisterPool(numberEnemySpawn);
            }
        }
    }
    public override void Spawn() {
        if (!isForceSpawn) {
            base.Spawn();
        }
    }
    private float GetMultiplerValue() {
        return GameManager.Instance.GameController.GetDifficultMultiple();
    }

    public void SpawnEnemy() {
        for (int i = 0; i < numberEnemySpawn; ++i) {
            E01Base e = GameManager.Instance.GameLoader.SpawnEnemy(enemySpawnAfterDead, transform.position);
            if (e) {
                e.ChangedStatWithMultipler(GetMultiplerValue());
                e.Initialize();
                e.E01Skin.SetSkin(E01Skin.GetSkin());
                e.EnemyStat.Size.SetBaseValue(EnemyStat.Size.Value);
                e.UpdateSize();
            }
        }
    }
    public override void Die() {
        SpawnEnemy();
        base.Die();
    }

    public override void SelfDestruction() {
        if (canDropChip) {
            ChooseEnemyDropChip();
        }
        base.SelfDestruction();
    }

    private void ChooseEnemyDropChip() {
        List<EnemyBase> enemies = GameManager.Instance.GameLoader.Enemies;
        RandomHelper.Shuffle(enemies);
        for (int i = 0; i < enemies.Count; ++i) {
            EnemyBase e = enemies[i];
            if (e != null && !e.IsDie() && e.EnableDropChip && !e.CanDropChip) {
                e.CanDropChip = true;
                return;
            }
        }
    }

    public override void Initialize() {
        base.Initialize();
        E01Skin.Initalize();
    }
}
