

public class E04Base : EnemyBase {
    #region References
    private E04Attack e04Attack;
    public E04Attack E04Attack {
        get {
            if (e04Attack == null) {
                e04Attack = EnemyAttack as E04Attack;
            }
            return e04Attack;
        }
    }

    private E04Move e04Move;
    public E04Move E04Move {
        get {
            if (e04Move == null) {
                e04Move = EnemyMove as E04Move;
            }
            return e04Move;
        }
    }

    private E04Health e04Health;
    public E04Health E04Health {
        get {
            if (e04Health == null) {
                e04Health = EnemyHealth as E04Health;
            }
            return e04Health;
        }
    }

    private E04Stat e04Stat;
    public E04Stat E04Stat {
        get {
            if (e04Stat == null) {
                e04Stat = EnemyStat as E04Stat;
            }
            return e04Stat;
        }
    }

    private E04Hitbox e04Hitbox;
    public E04Hitbox E04Hitbox {
        get {
            if (e04Hitbox == null) {
                e04Hitbox = EnemyHitbox as E04Hitbox;
            }
            return e04Hitbox;
        }
    }

    private E04Skill e04Skill;
    public E04Skill E04Skill {
        get {
            if (e04Skill == null) {
                e04Skill = EnemySkill as E04Skill;
            }
            return e04Skill;
        }
    }

    #endregion

    public override void ChangeStatWithEventValue(float atkPercent, float hpPercent, float size) {
        base.ChangeStatWithEventValue(atkPercent, hpPercent, size);
        E04Move.SetSizeTrail(size);
    }
}
