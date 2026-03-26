
using UnityEngine;

[RequireComponent(typeof(E16Attack))]
[RequireComponent(typeof(E16Move))]
[RequireComponent(typeof(E16Health))]
[RequireComponent(typeof(E16Stat))]
[RequireComponent(typeof(E16Hitbox))]
[RequireComponent(typeof(E16Skill))]
[RequireComponent(typeof(E16Effect))]
[RequireComponent(typeof(E16StateController))]
public class E16Base : EnemyBase {
    #region References
    private E16Attack e16Attack;
    public E16Attack E16Attack {
        get {
            if (e16Attack == null) {
                e16Attack = EnemyAttack as E16Attack;
            }
            return e16Attack;
        }
    }

    private E16Move e16Move;
    public E16Move E16Move {
        get {
            if (e16Move == null) {
                e16Move = EnemyMove as E16Move;
            }
            return e16Move;
        }
    }

    private E16Health e16Health;
    public E16Health E16Health {
        get {
            if (e16Health == null) {
                e16Health = EnemyHealth as E16Health;
            }
            return e16Health;
        }
    }

    private E16Stat e16Stat;
    public E16Stat E16Stat {
        get {
            if (e16Stat == null) {
                e16Stat = EnemyStat as E16Stat;
            }
            return e16Stat;
        }
    }

    private E16Hitbox e16Hitbox;
    public E16Hitbox E16Hitbox {
        get {
            if (e16Hitbox == null) {
                e16Hitbox = EnemyHitbox as E16Hitbox;
            }
            return e16Hitbox;
        }
    }

    private E16Skill e16Skill;
    public E16Skill E16Skill {
        get {
            if (e16Skill == null) {
                e16Skill = EnemySkill as E16Skill;
            }
            return e16Skill;
        }
    }

    private E16Effect e16Effect;
    public E16Effect E16Effect {
        get {
            if (e16Effect == null) {
                e16Effect = EnemyEffect as E16Effect;
            }
            return e16Effect;
        }
    }

    #endregion
}
