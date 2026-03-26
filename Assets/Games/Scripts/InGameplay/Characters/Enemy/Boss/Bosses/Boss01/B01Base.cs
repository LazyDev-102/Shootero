

public class B01Base : BossBase {
    #region References
    private B01Attack b01Attack;
    public B01Attack B01Attack {
        get {
            if(b01Attack == null) {
                b01Attack = BossAttack as B01Attack;
            }
            return b01Attack;
        }
    }

    private B01Move b01Move;
    public B01Move B01Move {
        get {
            if(b01Move == null) {
                b01Move = BossMove as B01Move;
            }
            return b01Move;
        }
    }

    private B01Health b01Health;
    public B01Health B01Health {
        get {
            if(b01Health == null) {
                b01Health = BossHealth as B01Health;
            }
            return b01Health;
        }
    }

    private B01Stat b01Stat;
    public B01Stat B01Stat {
        get {
            if(b01Stat == null) {
                b01Stat = BossStat as B01Stat;
            }
            return b01Stat;
        }
    }

    private B01Hitbox b01Hitbox;
    public B01Hitbox B01Hitbox {
        get {
            if(b01Hitbox == null) {
                b01Hitbox = BossHitbox as B01Hitbox;
            }
            return b01Hitbox;
        }
    }

    private B01Skill b01Skill;
    public B01Skill B01Skill {
        get {
            if(b01Skill == null) {
                b01Skill = BossSkill as B01Skill;
            }
            return b01Skill;
        }
    }
    #endregion
}
