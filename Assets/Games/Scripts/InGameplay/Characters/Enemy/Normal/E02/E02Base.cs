using UnityEngine;

[RequireComponent(typeof(E02Attack))]
[RequireComponent(typeof(E02Move))]
[RequireComponent(typeof(E02Health))]
[RequireComponent(typeof(E02Stat))]
[RequireComponent(typeof(E02Hitbox))]
[RequireComponent(typeof(E02Skill))]
[RequireComponent(typeof(E02StateController))]

public class E02Base : EnemyBase {
    #region References
    private E02Attack e02Attack;
    public E02Attack E02Attack {
        get {
            if (e02Attack == null) {
                e02Attack = EnemyAttack as E02Attack;
            }
            return e02Attack;
        }
    }

    private E02Move e02Move;
    public E02Move E02Move {
        get {
            if (e02Move == null) {
                e02Move = EnemyMove as E02Move;
            }
            return e02Move;
        }
    }

    private E02Health e02Health;
    public E02Health E02Health {
        get {
            if (e02Health == null) {
                e02Health = EnemyHealth as E02Health;
            }
            return e02Health;
        }
    }

    private E02Stat e02Stat;
    public E02Stat E02Stat {
        get {
            if (e02Stat == null) {
                e02Stat = EnemyStat as E02Stat;
            }
            return e02Stat;
        }
    }

    private E02Hitbox e02Hitbox;
    public E02Hitbox E02Hitbox {
        get {
            if (e02Hitbox == null) {
                e02Hitbox = EnemyHitbox as E02Hitbox;
            }
            return e02Hitbox;
        }
    }

    private E02Skill e02Skill;
    public E02Skill E02Skill {
        get {
            if (e02Skill == null) {
                e02Skill = EnemySkill as E02Skill;
            }
            return e02Skill;
        }
    }

    #endregion

    public override void ChangeStatWithEventValue(float atkPercent, float hpPercent, float size) {
        base.ChangeStatWithEventValue(atkPercent, hpPercent, size);
        E02Move.SetSizeTrail(size);
    }
}
