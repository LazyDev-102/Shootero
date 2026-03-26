using UnityEngine;
[RequireComponent(typeof(B11Attack), typeof(B11Health), typeof(B11Move))]
[RequireComponent(typeof(B11Skill), typeof(B11Stat), typeof(B11HitBox))]
[RequireComponent(typeof(B11StateController), typeof(B11Effect))]
public class B11Base : BossBase {
    #region References
    private B11Attack b11Attack;
    public B11Attack B11Attack {
        get {
            if (b11Attack == null) {
                b11Attack = BossAttack as B11Attack;
            }
            return b11Attack;
        }
    }

    private B11Move b11Move;
    public B11Move B11Move {
        get {
            if (b11Move == null) {
                b11Move = BossMove as B11Move;
            }
            return b11Move;
        }
    }

    private B11Health b11Health;
    public B11Health B11Health {
        get {
            if (b11Health == null) {
                b11Health = BossHealth as B11Health;
            }
            return b11Health;
        }
    }

    private B11Stat b11Stat;
    public B11Stat B11Stat {
        get {
            if (b11Stat == null) {
                b11Stat = BossStat as B11Stat;
            }
            return b11Stat;
        }
    }

    private B11HitBox b11Hitbox;
    public B11HitBox B11Hitbox {
        get {
            if (b11Hitbox == null) {
                b11Hitbox = BossHitbox as B11HitBox;
            }
            return b11Hitbox;
        }
    }

    private B11Skill b11Skill;
    public B11Skill B11Skill {
        get {
            if (b11Skill == null) {
                b11Skill = BossSkill as B11Skill;
            }
            return b11Skill;
        }
    }
    #endregion
}
