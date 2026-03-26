
using UnityEngine;

[RequireComponent(typeof(E17Attack))]
[RequireComponent(typeof(E17Move))]
[RequireComponent(typeof(E17Health))]
[RequireComponent(typeof(E17Stat))]
[RequireComponent(typeof(E17Hitbox))]
[RequireComponent(typeof(E17Skill))]
[RequireComponent(typeof(E17Effect))]
[RequireComponent(typeof(E17StateController))]
public class E17Base : EnemyBase {
    #region References
    private E17Attack e17Attack;
    public E17Attack E17Attack {
        get {
            if (e17Attack == null) {
                e17Attack = EnemyAttack as E17Attack;
            }
            return e17Attack;
        }
    }

    private E17Move e17Move;
    public E17Move E17Move {
        get {
            if (e17Move == null) {
                e17Move = EnemyMove as E17Move;
            }
            return e17Move;
        }
    }

    private E17Health e17Health;
    public E17Health E17Health {
        get {
            if (e17Health == null) {
                e17Health = EnemyHealth as E17Health;
            }
            return e17Health;
        }
    }

    private E17Stat e17Stat;
    public E17Stat E17Stat {
        get {
            if (e17Stat == null) {
                e17Stat = EnemyStat as E17Stat;
            }
            return e17Stat;
        }
    }

    private E17Hitbox e17Hitbox;
    public E17Hitbox E17Hitbox {
        get {
            if (e17Hitbox == null) {
                e17Hitbox = EnemyHitbox as E17Hitbox;
            }
            return e17Hitbox;
        }
    }

    private E17Skill e17Skill;
    public E17Skill E17Skill {
        get {
            if (e17Skill == null) {
                e17Skill = EnemySkill as E17Skill;
            }
            return e17Skill;
        }
    }

    private E17Effect e17Effect;
    public E17Effect E17Effect {
        get {
            if (e17Effect == null) {
                e17Effect = EnemyEffect as E17Effect;
            }
            return e17Effect;
        }
    }

    #endregion
}
