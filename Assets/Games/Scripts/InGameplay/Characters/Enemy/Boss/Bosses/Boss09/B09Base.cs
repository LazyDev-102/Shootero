using UnityEngine;
[RequireComponent(typeof(B09Attack), typeof(B09Health), typeof(B09Move))]
[RequireComponent(typeof(B09Skill), typeof(B09Stat), typeof(B09HitBox))]
public class B09Base : BossBase {
    #region References
    private B09Attack b09Attack;
    public B09Attack B09Attack {
        get {
            if(b09Attack == null) {
                b09Attack = BossAttack as B09Attack;
            }
            return b09Attack;
        }
    }

    private B09Move b09Move;
    public B09Move B09Move {
        get {
            if(b09Move == null) {
                b09Move = BossMove as B09Move;
            }
            return b09Move;
        }
    }

    private B09Health b09Health;
    public B09Health B09Health {
        get {
            if(b09Health == null) {
                b09Health = BossHealth as B09Health;
            }
            return b09Health;
        }
    }

    private B09Stat b09Stat;
    public B09Stat B09Stat {
        get {
            if(b09Stat == null) {
                b09Stat = BossStat as B09Stat;
            }
            return b09Stat;
        }
    }

    private B09HitBox b09Hitbox;
    public B09HitBox B09Hitbox {
        get {
            if(b09Hitbox == null) {
                b09Hitbox = BossHitbox as B09HitBox;
            }
            return b09Hitbox;
        }
    }

    private B09Skill b09Skill;
    public B09Skill B09Skill {
        get {
            if(b09Skill == null) {
                b09Skill = BossSkill as B09Skill;
            }
            return b09Skill;
        }
    }
    #endregion
}
