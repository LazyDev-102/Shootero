
using UnityEngine;

[RequireComponent(typeof(E12Attack))]
[RequireComponent(typeof(E12Move))]
[RequireComponent(typeof(E12Health))]
[RequireComponent(typeof(E12Stat))]
[RequireComponent(typeof(E12Hitbox))]
[RequireComponent(typeof(E12Skill))]
[RequireComponent(typeof(E12StateController))]
public class E12Base : EnemyBase {
    #region References
    private E12Attack e12Attack;
    public E12Attack E12Attack {
        get {
            if (e12Attack == null) {
                e12Attack = EnemyAttack as E12Attack;
            }
            return e12Attack;
        }
    }

    private E12Move e12Move;
    public E12Move E12Move {
        get {
            if (e12Move == null) {
                e12Move = EnemyMove as E12Move;
            }
            return e12Move;
        }
    }

    private E12Health e12Health;
    public E12Health E12Health {
        get {
            if (e12Health == null) {
                e12Health = EnemyHealth as E12Health;
            }
            return e12Health;
        }
    }

    private E12Stat e12Stat;
    public E12Stat E12Stat {
        get {
            if (e12Stat == null) {
                e12Stat = EnemyStat as E12Stat;
            }
            return e12Stat;
        }
    }

    private E12Hitbox e12Hitbox;
    public E12Hitbox E12Hitbox {
        get {
            if (e12Hitbox == null) {
                e12Hitbox = EnemyHitbox as E12Hitbox;
            }
            return e12Hitbox;
        }
    }

    private E12Skill e12Skill;
    public E12Skill E12Skill {
        get {
            if (e12Skill == null) {
                e12Skill = EnemySkill as E12Skill;
            }
            return e12Skill;
        }
    }

    #endregion

    #region Spawn On Die
    [SerializeField] private int childHP = 1000;
    [SerializeField] private int childDamage = 50;
    [SerializeField] private int childCount;
    [SerializeField] private E12Base childPrefab;

    private Vector2 spawnPosition;
    public override void Die() {
        SpawnChild();
        base.Die();
    }
    private void SpawnChild() {
        if (childCount == 0)
            return;
        for (int i = 0; i < childCount; i++) {
            E12Base mbClone = GameManager.Instance.GameLoader.SpawnEnemy(childPrefab, transform.position);
            mbClone.E12Stat.MaxHP.SetBaseValue(childHP);
            mbClone.E12Stat.Atk.SetBaseValue(childDamage);
            mbClone.transform.localPosition = transform.localPosition;
            mbClone.Initialize();
        }
    }
    #endregion
}
