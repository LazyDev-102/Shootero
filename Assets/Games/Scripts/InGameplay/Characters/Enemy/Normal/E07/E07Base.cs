
using UnityEngine;

[RequireComponent(typeof(E07Attack))]
[RequireComponent(typeof(E07Move))]
[RequireComponent(typeof(E07Health))]
[RequireComponent(typeof(E07Stat))]
[RequireComponent(typeof(E07Hitbox))]
[RequireComponent(typeof(E07Skill))]
[RequireComponent(typeof(E07StateController))]
public class E07Base : EnemyBase {
    #region References
    private E07Attack e07Attack;
    public E07Attack E07Attack {
        get {
            if (e07Attack == null) {
                e07Attack = EnemyAttack as E07Attack;
            }
            return e07Attack;
        }
    }

    private E07Move e07Move;
    public E07Move E07Move {
        get {
            if (e07Move == null) {
                e07Move = EnemyMove as E07Move;
            }
            return e07Move;
        }
    }

    private E07Health e07Health;
    public E07Health E07Health {
        get {
            if (e07Health == null) {
                e07Health = EnemyHealth as E07Health;
            }
            return e07Health;
        }
    }

    private E07Stat e07Stat;
    public E07Stat E07Stat {
        get {
            if (e07Stat == null) {
                e07Stat = EnemyStat as E07Stat;
            }
            return e07Stat;
        }
    }

    private E07Hitbox e07Hitbox;
    public E07Hitbox E07Hitbox {
        get {
            if (e07Hitbox == null) {
                e07Hitbox = EnemyHitbox as E07Hitbox;
            }
            return e07Hitbox;
        }
    }

    private E07Skill e07Skill;
    public E07Skill E07Skill {
        get {
            if (e07Skill == null) {
                e07Skill = EnemySkill as E07Skill;
            }
            return e07Skill;
        }
    }

    private E07Effect e07Effect;
    public E07Effect E07Effect {
        get {
            if (e07Effect == null) {
                e07Effect = EnemyEffect as E07Effect;
            }
            return e07Effect;
        }
    }
    #endregion

    public override void ChangeStatWithEventValue(float atkPercent, float hpPercent, float size) {
        base.ChangeStatWithEventValue(atkPercent, hpPercent, size);
        E07Move.SetSizeTrail(size);
    }
}
