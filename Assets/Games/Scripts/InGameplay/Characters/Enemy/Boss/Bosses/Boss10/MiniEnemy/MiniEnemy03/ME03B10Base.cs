using Helper;
using System;
using UnityEngine;

public class ME03B10Base : EnemyBase {
    #region References
    private ME03B10Attack me03B10Attack;
    public ME03B10Attack ME03B10Attack {
        get {
            if (me03B10Attack == null) {
                me03B10Attack = EnemyAttack as ME03B10Attack;
            }
            return me03B10Attack;
        }
    }

    private ME03B10Move me03B10Move;
    public ME03B10Move ME03B10Move {
        get {
            if (me03B10Move == null) {
                me03B10Move = EnemyMove as ME03B10Move;
            }
            return me03B10Move;
        }
    }

    private ME03B10Health me03B10Health;
    public ME03B10Health ME03B10Health {
        get {
            if (me03B10Health == null) {
                me03B10Health = EnemyHealth as ME03B10Health;
            }
            return me03B10Health;
        }
    }

    private ME03B10Stat me03B10Stat;
    public ME03B10Stat ME03B10Stat {
        get {
            if (me03B10Stat == null) {
                me03B10Stat = EnemyStat as ME03B10Stat;
            }
            return me03B10Stat;
        }
    }

    private ME03B10Hitbox me03B10Hitbox;
    public ME03B10Hitbox ME03B10Hitbox {
        get {
            if (me03B10Hitbox == null) {
                me03B10Hitbox = EnemyHitbox as ME03B10Hitbox;
            }
            return me03B10Hitbox;
        }
    }

    private ME03B10Skill me03B10Skill;
    public ME03B10Skill ME03B10Skill {
        get {
            if (me03B10Skill == null) {
                me03B10Skill = EnemySkill as ME03B10Skill;
            }
            return me03B10Skill;
        }
    }
    #endregion


    private B10Base bossParent;
    private ME03B10Base myBrother;
    private bool isBigBrother;
    private Action<ME03B10Base> onMEDie;

    public void SetParentBoss(B10Base boss) {
        bossParent = boss;
    }

    public void SetBrother(ME03B10Base bro) {
        myBrother = bro;
    }

    public ME03B10Base GetBrother() {
        return myBrother;
    }

    public void SetBigBrother(bool isBig) {
        isBigBrother = isBig;
    }

    public bool GetIsBigBrother() {
        return isBigBrother;
    }

    public void AddOnMEDie(Action<ME03B10Base> onAction) {
        this.onMEDie = onAction;
    }

    public void MeDie() {
        onMEDie?.Invoke(this);
        onMEDie = null;
        if (myBrother != null && !myBrother.IsDie()) {
            myBrother.ME03B10Health.ForceChangeCurrentHp(0);
        }
    }

    public override void Die() {
        MeDie();
        base.Die();
    }
}
