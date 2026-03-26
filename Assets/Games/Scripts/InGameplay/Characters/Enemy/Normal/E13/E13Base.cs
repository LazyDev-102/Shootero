
using UnityEngine;

[RequireComponent(typeof(E13Attack))]
[RequireComponent(typeof(E13Move))]
[RequireComponent(typeof(E13Health))]
[RequireComponent(typeof(E13Stat))]
[RequireComponent(typeof(E13Hitbox))]
[RequireComponent(typeof(E13Skill))]
[RequireComponent(typeof(E13Effect))]
[RequireComponent(typeof(E13StateController))]
public class E13Base : EnemyBase {
    #region References
    private E13Attack e13Attack;
    public E13Attack E13Attack {
        get {
            if (e13Attack == null) {
                e13Attack = EnemyAttack as E13Attack;
            }
            return e13Attack;
        }
    }

    private E13Move e13Move;
    public E13Move E13Move {
        get {
            if (e13Move == null) {
                e13Move = EnemyMove as E13Move;
            }
            return e13Move;
        }
    }

    private E13Health e13Health;
    public E13Health E13Health {
        get {
            if (e13Health == null) {
                e13Health = EnemyHealth as E13Health;
            }
            return e13Health;
        }
    }

    private E13Stat e13Stat;
    public E13Stat E13Stat {
        get {
            if (e13Stat == null) {
                e13Stat = EnemyStat as E13Stat;
            }
            return e13Stat;
        }
    }

    private E13Hitbox e13Hitbox;
    public E13Hitbox E13Hitbox {
        get {
            if (e13Hitbox == null) {
                e13Hitbox = EnemyHitbox as E13Hitbox;
            }
            return e13Hitbox;
        }
    }

    private E13Skill e13Skill;
    public E13Skill E13Skill {
        get {
            if (e13Skill == null) {
                e13Skill = EnemySkill as E13Skill;
            }
            return e13Skill;
        }
    }

    private E13Effect e13Effect;
    public E13Effect E13Effect {
        get {
            if (e13Effect == null) {
                e13Effect = EnemyEffect as E13Effect;
            }
            return e13Effect;
        }
    }

    #endregion
}
