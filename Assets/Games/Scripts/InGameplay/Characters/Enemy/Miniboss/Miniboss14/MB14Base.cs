using UnityEngine;

public class MB14Base : MinibossBase {
    #region References
    private MB14Attack mb14Attack;
    public MB14Attack MB14Attack {
        get {
            if (mb14Attack == null) {
                mb14Attack = EnemyAttack as MB14Attack;
            }
            return mb14Attack;
        }
    }

    private MB14Move mb14Move;
    public MB14Move MB14Move {
        get {
            if (mb14Move == null) {
                mb14Move = EnemyMove as MB14Move;
            }
            return mb14Move;
        }
    }

    private MB14Health mb14Health;
    public MB14Health MB14Health {
        get {
            if (mb14Health == null) {
                mb14Health = EnemyHealth as MB14Health;
            }
            return mb14Health;
        }
    }

    private MB14Stat mb14Stat;
    public MB14Stat MB14Stat {
        get {
            if (mb14Stat == null) {
                mb14Stat = EnemyStat as MB14Stat;
            }
            return mb14Stat;
        }
    }

    private MB14Hitbox mb14Hitbox;
    public MB14Hitbox MB14Hitbox {
        get {
            if (mb14Hitbox == null) {
                mb14Hitbox = EnemyHitbox as MB14Hitbox;
            }
            return mb14Hitbox;
        }
    }

    private MB14Skill mb14Skill;
    public MB14Skill MB14Skill {
        get {
            if (mb14Skill == null) {
                mb14Skill = EnemySkill as MB14Skill;
            }
            return mb14Skill;
        }
    }
    #endregion
}
