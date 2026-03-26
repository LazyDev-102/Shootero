using UnityEngine;

[RequireComponent(typeof(B12ChildAttack))]
[RequireComponent(typeof(B12ChildMove))]
[RequireComponent(typeof(B12ChildHealth))]
[RequireComponent(typeof(B12ChildStat))]
[RequireComponent(typeof(B12ChildHitbox))]
[RequireComponent(typeof(B12ChildSkill))]
[RequireComponent(typeof(B12ChildStateController))]

public class B12ChildBase : EnemyBase {
    #region References
    private B12ChildAttack e02Attack;
    public B12ChildAttack B12ChildAttack {
        get {
            if (e02Attack == null) {
                e02Attack = EnemyAttack as B12ChildAttack;
            }
            return e02Attack;
        }
    }

    private B12ChildMove e02Move;
    public B12ChildMove B12ChildMove {
        get {
            if (e02Move == null) {
                e02Move = EnemyMove as B12ChildMove;
            }
            return e02Move;
        }
    }

    private B12ChildHealth e02Health;
    public B12ChildHealth B12ChildHealth {
        get {
            if (e02Health == null) {
                e02Health = EnemyHealth as B12ChildHealth;
            }
            return e02Health;
        }
    }

    private B12ChildStat e02Stat;
    public B12ChildStat B12ChildStat {
        get {
            if (e02Stat == null) {
                e02Stat = EnemyStat as B12ChildStat;
            }
            return e02Stat;
        }
    }

    private B12ChildHitbox e02Hitbox;
    public B12ChildHitbox B12ChildHitbox {
        get {
            if (e02Hitbox == null) {
                e02Hitbox = EnemyHitbox as B12ChildHitbox;
            }
            return e02Hitbox;
        }
    }

    private B12ChildSkill e02Skill;
    public B12ChildSkill B12ChildSkill {
        get {
            if (e02Skill == null) {
                e02Skill = EnemySkill as B12ChildSkill;
            }
            return e02Skill;
        }
    }

    #endregion

    public override void ChangeStatWithEventValue(float atkPercent, float hpPercent, float size) {
        base.ChangeStatWithEventValue(atkPercent, hpPercent, size);
        B12ChildMove.SetSizeTrail(size);
    }
}
