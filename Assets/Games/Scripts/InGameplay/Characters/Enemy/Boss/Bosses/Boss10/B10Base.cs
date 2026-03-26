



public class B10Base : BossBase {
    #region References
    private B10Attack b10Attack;
    public B10Attack B10Attack {
        get {
            if (b10Attack == null) {
                b10Attack = BossAttack as B10Attack;
            }
            return b10Attack;
        }
    }

    private B10Move b10Move;
    public B10Move B10Move {
        get {
            if (b10Move == null) {
                b10Move = BossMove as B10Move;
            }
            return b10Move;
        }
    }

    private B10Health b10Health;
    public B10Health B10Health {
        get {
            if (b10Health == null) {
                b10Health = BossHealth as B10Health;
            }
            return b10Health;
        }
    }

    private B10Stat b10Stat;
    public B10Stat B10Stat {
        get {
            if (b10Stat == null) {
                b10Stat = BossStat as B10Stat;
            }
            return b10Stat;
        }
    }

    private B10Hitbox b10Hitbox;
    public B10Hitbox B10Hitbox {
        get {
            if (b10Hitbox == null) {
                b10Hitbox = BossHitbox as B10Hitbox;
            }
            return b10Hitbox;
        }
    }

    private B10Skill b10Skill;
    public B10Skill B10Skill {
        get {
            if (b10Skill == null) {
                b10Skill = BossSkill as B10Skill;
            }
            return b10Skill;
        }
    }

    #endregion



}
