
using UnityEngine;

[RequireComponent(typeof(E06Attack))]
[RequireComponent(typeof(E06Move))]
[RequireComponent(typeof(E06Health))]
[RequireComponent(typeof(E06Stat))]
[RequireComponent(typeof(E06Hitbox))]
[RequireComponent(typeof(E06Skill))]
[RequireComponent(typeof(E06StateController))]
public class E06Base : EnemyBase {
    #region References
    private E06Attack e06Attack;
    public E06Attack E06Attack {
        get {
            if(e06Attack == null) {
                e06Attack = EnemyAttack as E06Attack;
            }
            return e06Attack;
        }
    }

    private E06Move e06Move;
    public E06Move E06Move {
        get {
            if(e06Move == null) {
                e06Move = EnemyMove as E06Move;
            }
            return e06Move;
        }
    }

    private E06Health e06Health;
    public E06Health E06Health {
        get {
            if(e06Health == null) {
                e06Health = EnemyHealth as E06Health;
            }
            return e06Health;
        }
    }

    private E06Stat e06Stat;
    public E06Stat E06Stat {
        get {
            if(e06Stat == null) {
                e06Stat = EnemyStat as E06Stat;
            }
            return e06Stat;
        }
    }

    private E06Hitbox e06Hitbox;
    public E06Hitbox E06Hitbox {
        get {
            if(e06Hitbox == null) {
                e06Hitbox = EnemyHitbox as E06Hitbox;
            }
            return e06Hitbox;
        }
    }

    private E06Skill e06Skill;
    public E06Skill E06Skill {
        get {
            if(e06Skill == null) {
                e06Skill = EnemySkill as E06Skill;
            }
            return e06Skill;
        }
    }

    #endregion
}
