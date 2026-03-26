using UnityEngine;

[RequireComponent(typeof(E03Attack))]
[RequireComponent(typeof(E03Move))]
[RequireComponent(typeof(E03Health))]
[RequireComponent(typeof(E03Stat))]
[RequireComponent(typeof(E03Hitbox))]
[RequireComponent(typeof(E03Skill))]
[RequireComponent(typeof(E03StateController))]

public class E03Base : EnemyBase{
    #region References
    private E03Attack e03Attack;
    public E03Attack E03Attack {
        get {
            if(e03Attack == null) {
                e03Attack = EnemyAttack as E03Attack;
            }
            return e03Attack;
        }
    }

    private E03Move e03Move;
    public E03Move E03Move {
        get {
            if(e03Move == null) {
                e03Move = EnemyMove as E03Move;
            }
            return e03Move;
        }
    }

    private E03Health e03Health;
    public E03Health E03Health {
        get {
            if(e03Health == null) {
                e03Health = EnemyHealth as E03Health;
            }
            return e03Health;
        }
    }

    private E03Stat e03Stat;
    public E03Stat E03Stat {
        get {
            if(e03Stat == null) {
                e03Stat = EnemyStat as E03Stat;
            }
            return e03Stat;
        }
    }

    private E03Hitbox e03Hitbox;
    public E03Hitbox E03Hitbox {
        get {
            if(e03Hitbox == null) {
                e03Hitbox = EnemyHitbox as E03Hitbox;
            }
            return e03Hitbox;
        }
    }

    private E03Skill e03Skill;
    public E03Skill E03Skill {
        get {
            if(e03Skill == null) {
                e03Skill = EnemySkill as E03Skill;
            }
            return e03Skill;
        }
    }

    #endregion
}
