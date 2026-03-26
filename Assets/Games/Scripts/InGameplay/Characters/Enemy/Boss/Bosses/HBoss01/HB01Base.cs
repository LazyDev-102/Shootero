

using UnityEngine;

[RequireComponent(typeof(HB01Attack), typeof(HB01Health), typeof(HB01Move))]
[RequireComponent(typeof(HB01Skill), typeof(HB01Stat), typeof(HB01Hitbox))]
public class HB01Base : BossBase {
    #region References
    private HB01Attack hb01Attack;
    public HB01Attack HB01Attack {
        get {
            if (hb01Attack == null) {
                hb01Attack = BossAttack as HB01Attack;
            }
            return hb01Attack;
        }
    }

    private HB01Move hb01Move;
    public HB01Move HB01Move {
        get {
            if (hb01Move == null) {
                hb01Move = BossMove as HB01Move;
            }
            return hb01Move;
        }
    }

    private HB01Health hb01Health;
    public HB01Health HB01Health {
        get {
            if (hb01Health == null) {
                hb01Health = BossHealth as HB01Health;
            }
            return hb01Health;
        }
    }

    private HB01Stat hb01Stat;
    public HB01Stat HB01Stat {
        get {
            if (hb01Stat == null) {
                hb01Stat = BossStat as HB01Stat;
            }
            return hb01Stat;
        }
    }

    private HB01Hitbox hb01Hitbox;
    public HB01Hitbox HB01Hitbox {
        get {
            if (hb01Hitbox == null) {
                hb01Hitbox = BossHitbox as HB01Hitbox;
            }
            return hb01Hitbox;
        }
    }

    private HB01Skill hb01Skill;
    public HB01Skill HB01Skill {
        get {
            if (hb01Skill == null) {
                hb01Skill = BossSkill as HB01Skill;
            }
            return hb01Skill;
        }
    }
    #endregion
}
