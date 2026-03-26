
using UnityEngine;

[RequireComponent(typeof(E14Attack))]
[RequireComponent(typeof(E14Move))]
[RequireComponent(typeof(E14Health))]
[RequireComponent(typeof(E14Stat))]
[RequireComponent(typeof(E14Hitbox))]
[RequireComponent(typeof(E14Skill))]
[RequireComponent(typeof(E14Effect))]
[RequireComponent(typeof(E14StateController))]
public class E14Base : EnemyBase {
    #region References
    private E14Attack e14Attack;
    public E14Attack E14Attack {
        get {
            if (e14Attack == null) {
                e14Attack = EnemyAttack as E14Attack;
            }
            return e14Attack;
        }
    }

    private E14Move e14Move;
    public E14Move E14Move {
        get {
            if (e14Move == null) {
                e14Move = EnemyMove as E14Move;
            }
            return e14Move;
        }
    }

    private E14Health e14Health;
    public E14Health E14Health {
        get {
            if (e14Health == null) {
                e14Health = EnemyHealth as E14Health;
            }
            return e14Health;
        }
    }

    private E14Stat e14Stat;
    public E14Stat E14Stat {
        get {
            if (e14Stat == null) {
                e14Stat = EnemyStat as E14Stat;
            }
            return e14Stat;
        }
    }

    private E14Hitbox e14Hitbox;
    public E14Hitbox E14Hitbox {
        get {
            if (e14Hitbox == null) {
                e14Hitbox = EnemyHitbox as E14Hitbox;
            }
            return e14Hitbox;
        }
    }

    private E14Skill e14Skill;
    public E14Skill E14Skill {
        get {
            if (e14Skill == null) {
                e14Skill = EnemySkill as E14Skill;
            }
            return e14Skill;
        }
    }

    private E14Effect e14Effect;
    public E14Effect E14Effect {
        get {
            if (e14Effect == null) {
                e14Effect = EnemyEffect as E14Effect;
            }
            return e14Effect;
        }
    }

    #endregion
}
