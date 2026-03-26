

using System;

public class MERageB08Base : EnemyBase {
    #region References
    private MERageB08Attack meRageB08Attack;
    public MERageB08Attack MERageB08Attack {
        get {
            if (meRageB08Attack == null) {
                meRageB08Attack = EnemyAttack as MERageB08Attack;
            }
            return meRageB08Attack;
        }
    }

    private MERageB08Move meRageB08Move;
    public MERageB08Move MERageB08Move {
        get {
            if (meRageB08Move == null) {
                meRageB08Move = EnemyMove as MERageB08Move;
            }
            return meRageB08Move;
        }
    }

    private MERageB08Health meRageB08Health;
    public MERageB08Health MERageB08Health {
        get {
            if (meRageB08Health == null) {
                meRageB08Health = EnemyHealth as MERageB08Health;
            }
            return meRageB08Health;
        }
    }

    private MERageB08Stat meRageB08Stat;
    public MERageB08Stat MERageB08Stat {
        get {
            if (meRageB08Stat == null) {
                meRageB08Stat = EnemyStat as MERageB08Stat;
            }
            return meRageB08Stat;
        }
    }

    private MERageB08Hitbox meRageB08Hitbox;
    public MERageB08Hitbox MERageB08Hitbox {
        get {
            if (meRageB08Hitbox == null) {
                meRageB08Hitbox = EnemyHitbox as MERageB08Hitbox;
            }
            return meRageB08Hitbox;
        }
    }

    private MERageB08Skill meRageB08Skill;
    public MERageB08Skill MERageB08Skill {
        get {
            if (meRageB08Skill == null) {
                meRageB08Skill = EnemySkill as MERageB08Skill;
            }
            return meRageB08Skill;
        }
    }
    #endregion

    private Action<MERageB08Base> onMEDead;
    public override void Die() {
        base.Die();
        onMEDead?.Invoke(this);
        onMEDead = null;
    }

    public void AddOnMEDead(Action<MERageB08Base> action) {
        onMEDead = action;
    }

    public void SetInfo(int hp, float valueHeal) {
        MERageB08Stat.MaxHP.SetBaseValue(hp, true);
        MERageB08Attack.SetValueHeal(valueHeal);
    }

}
