

public class B07Base : BossBase {
    #region References
    private B07Attack b07Attack;
    public B07Attack B07Attack {
        get {
            if (b07Attack == null) {
                b07Attack = BossAttack as B07Attack;
            }
            return b07Attack;
        }
    }

    private B07Move b07Move;
    public B07Move B07Move {
        get {
            if (b07Move == null) {
                b07Move = BossMove as B07Move;
            }
            return b07Move;
        }
    }

    private B07Health b07Health;
    public B07Health B07Health {
        get {
            if (b07Health == null) {
                b07Health = BossHealth as B07Health;
            }
            return b07Health;
        }
    }

    private B07Stat b07Stat;
    public B07Stat B07Stat {
        get {
            if (b07Stat == null) {
                b07Stat = BossStat as B07Stat;
            }
            return b07Stat;
        }
    }

    private B07Hitbox b07Hitbox;
    public B07Hitbox B07Hitbox {
        get {
            if (b07Hitbox == null) {
                b07Hitbox = BossHitbox as B07Hitbox;
            }
            return b07Hitbox;
        }
    }

    private B07Skill b07Skill;
    public B07Skill B07Skill {
        get {
            if (b07Skill == null) {
                b07Skill = BossSkill as B07Skill;
            }
            return b07Skill;
        }
    }
    #endregion
}
