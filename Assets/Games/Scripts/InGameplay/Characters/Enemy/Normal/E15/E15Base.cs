
using UnityEngine;

[RequireComponent(typeof(E15Attack))]
[RequireComponent(typeof(E15Move))]
[RequireComponent(typeof(E15Health))]
[RequireComponent(typeof(E15Stat))]
[RequireComponent(typeof(E15Hitbox))]
[RequireComponent(typeof(E15Skill))]
[RequireComponent(typeof(E15Effect))]
[RequireComponent(typeof(E15StateController))]
public class E15Base : EnemyBase {
    #region References
    private E15Attack e15Attack;
    public E15Attack E15Attack {
        get {
            if (e15Attack == null) {
                e15Attack = EnemyAttack as E15Attack;
            }
            return e15Attack;
        }
    }

    private E15Move e15Move;
    public E15Move E15Move {
        get {
            if (e15Move == null) {
                e15Move = EnemyMove as E15Move;
            }
            return e15Move;
        }
    }

    private E15Health e15Health;
    public E15Health E15Health {
        get {
            if (e15Health == null) {
                e15Health = EnemyHealth as E15Health;
            }
            return e15Health;
        }
    }

    private E15Stat e15Stat;
    public E15Stat E15Stat {
        get {
            if (e15Stat == null) {
                e15Stat = EnemyStat as E15Stat;
            }
            return e15Stat;
        }
    }

    private E15Hitbox e15Hitbox;
    public E15Hitbox E15Hitbox {
        get {
            if (e15Hitbox == null) {
                e15Hitbox = EnemyHitbox as E15Hitbox;
            }
            return e15Hitbox;
        }
    }

    private E15Skill e15Skill;
    public E15Skill E15Skill {
        get {
            if (e15Skill == null) {
                e15Skill = EnemySkill as E15Skill;
            }
            return e15Skill;
        }
    }

    private E15Effect e15Effect;
    public E15Effect E15Effect {
        get {
            if (e15Effect == null) {
                e15Effect = EnemyEffect as E15Effect;
            }
            return e15Effect;
        }
    }

    #endregion
}
