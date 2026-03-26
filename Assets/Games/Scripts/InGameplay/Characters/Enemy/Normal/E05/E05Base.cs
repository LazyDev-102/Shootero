
using UnityEngine;

[RequireComponent(typeof(E05Attack))]
[RequireComponent(typeof(E05Move))]
[RequireComponent(typeof(E05Health))]
[RequireComponent(typeof(E05Stat))]
[RequireComponent(typeof(E05Hitbox))]
[RequireComponent(typeof(E05Skill))]
[RequireComponent(typeof(E05StateController))]
public class E05Base : EnemyBase{
    #region References
    private E05Attack e05Attack;
    public E05Attack E05Attack {
        get {
            if(e05Attack == null) {
                e05Attack = EnemyAttack as E05Attack;
            }
            return e05Attack;
        }
    }

    private E05Move e05Move;
    public E05Move E05Move {
        get {
            if(e05Move == null) {
                e05Move = EnemyMove as E05Move;
            }
            return e05Move;
        }
    }

    private E05Health e05Health;
    public E05Health E05Health {
        get {
            if(e05Health == null) {
                e05Health = EnemyHealth as E05Health;
            }
            return e05Health;
        }
    }

    private E05Stat e05Stat;
    public E05Stat E05Stat {
        get {
            if(e05Stat == null) {
                e05Stat = EnemyStat as E05Stat;
            }
            return e05Stat;
        }
    }

    private E05Hitbox e05Hitbox;
    public E05Hitbox E05Hitbox {
        get {
            if(e05Hitbox == null) {
                e05Hitbox = EnemyHitbox as E05Hitbox;
            }
            return e05Hitbox;
        }
    }

    private E05Skill e05Skill;
    public E05Skill E05Skill {
        get {
            if(e05Skill == null) {
                e05Skill = EnemySkill as E05Skill;
            }
            return e05Skill;
        }
    }

    #endregion
}
