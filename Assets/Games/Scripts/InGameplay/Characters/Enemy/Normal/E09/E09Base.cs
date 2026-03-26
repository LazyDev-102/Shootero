
using UnityEngine;

[RequireComponent(typeof(E09Attack))]
[RequireComponent(typeof(E09Move))]
[RequireComponent(typeof(E09Health))]
[RequireComponent(typeof(E09Stat))]
[RequireComponent(typeof(E09Hitbox))]
[RequireComponent(typeof(E09Skill))]
[RequireComponent(typeof(E09StateController))]
public class E09Base : EnemyBase {
    #region References
    private E09Attack e09Attack;
    public E09Attack E09Attack {
        get {
            if(e09Attack == null) {
                e09Attack = EnemyAttack as E09Attack;
            }
            return e09Attack;
        }
    }

    private E09Move e09Move;
    public E09Move E09Move {
        get {
            if(e09Move == null) {
                e09Move = EnemyMove as E09Move;
            }
            return e09Move;
        }
    }

    private E09Health e09Health;
    public E09Health E09Health {
        get {
            if(e09Health == null) {
                e09Health = EnemyHealth as E09Health;
            }
            return e09Health;
        }
    }

    private E09Stat e09Stat;
    public E09Stat E09Stat {
        get {
            if(e09Stat == null) {
                e09Stat = EnemyStat as E09Stat;
            }
            return e09Stat;
        }
    }

    private E09Hitbox e09Hitbox;
    public E09Hitbox E09Hitbox {
        get {
            if(e09Hitbox == null) {
                e09Hitbox = EnemyHitbox as E09Hitbox;
            }
            return e09Hitbox;
        }
    }

    private E09Skill e09Skill;
    public E09Skill E09Skill {
        get {
            if(e09Skill == null) {
                e09Skill = EnemySkill as E09Skill;
            }
            return e09Skill;
        }
    }

    #endregion
}
