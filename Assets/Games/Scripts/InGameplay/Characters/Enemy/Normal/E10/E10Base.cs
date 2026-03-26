using UnityEngine;

[RequireComponent(typeof(E10Attack))]
[RequireComponent(typeof(E10Move))]
[RequireComponent(typeof(E10Health))]
[RequireComponent(typeof(E10Stat))]
[RequireComponent(typeof(E10Hitbox))]
[RequireComponent(typeof(E10Skill))]
[RequireComponent(typeof(E10StateController))]
public class E10Base : EnemyBase {
    #region References
    private E10Attack e10Attack;
    public E10Attack E10Attack {
        get {
            if(e10Attack == null) {
                e10Attack = EnemyAttack as E10Attack;
            }
            return e10Attack;
        }
    }

    private E10Move e10Move;
    public E10Move E10Move {
        get {
            if(e10Move == null) {
                e10Move = EnemyMove as E10Move;
            }
            return e10Move;
        }
    }

    private E10Health e10Health;
    public E10Health E10Health {
        get {
            if(e10Health == null) {
                e10Health = EnemyHealth as E10Health;
            }
            return e10Health;
        }
    }

    private E10Stat e10Stat;
    public E10Stat E10Stat {
        get {
            if(e10Stat == null) {
                e10Stat = EnemyStat as E10Stat;
            }
            return e10Stat;
        }
    }

    private E10Hitbox e10Hitbox;
    public E10Hitbox E10Hitbox {
        get {
            if(e10Hitbox == null) {
                e10Hitbox = EnemyHitbox as E10Hitbox;
            }
            return e10Hitbox;
        }
    }

    private E10Skill e10Skill;
    public E10Skill E10Skill {
        get {
            if(e10Skill == null) {
                e10Skill = EnemySkill as E10Skill;
            }
            return e10Skill;
        }
    }

    #endregion
}