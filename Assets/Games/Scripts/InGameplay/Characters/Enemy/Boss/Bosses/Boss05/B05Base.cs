using UnityEngine;
[RequireComponent(typeof(B05Attack), typeof(B05Health), typeof(B05Move))]
[RequireComponent(typeof(B05Skill), typeof(B05Stat), typeof(B05HitBox))]
public class B05Base : BossBase {
    #region References
    private B05Attack b05Attack;
    public B05Attack B05Attack {
        get {
            if(b05Attack == null) {
                b05Attack = BossAttack as B05Attack;
            }
            return b05Attack;
        }
    }

    private B05Move b05Move;
    public B05Move B05Move {
        get {
            if(b05Move == null) {
                b05Move = BossMove as B05Move;
            }
            return b05Move;
        }
    }

    private B05Health b05Health;
    public B05Health B05Health {
        get {
            if(b05Health == null) {
                b05Health = BossHealth as B05Health;
            }
            return b05Health;
        }
    }

    private B05Stat b05Stat;
    public B05Stat B05Stat {
        get {
            if(b05Stat == null) {
                b05Stat = BossStat as B05Stat;
            }
            return b05Stat;
        }
    }

    private B05HitBox b05Hitbox;
    public B05HitBox B05Hitbox {
        get {
            if(b05Hitbox == null) {
                b05Hitbox = BossHitbox as B05HitBox;
            }
            return b05Hitbox;
        }
    }

    private B05Skill b05Skill;
    public B05Skill B05Skill {
        get {
            if(b05Skill == null) {
                b05Skill = BossSkill as B05Skill;
            }
            return b05Skill;
        }
    }
    #endregion
}
