

using UnityEngine;

[RequireComponent(typeof(XB01Attack), typeof(XB01Health), typeof(XB01Move))]
[RequireComponent(typeof(XB01Skill), typeof(XB01Stat), typeof(XB01Hitbox))]
public class XB01Base : BossBase {
    #region References
    private XB01Attack xb01Attack;
    public XB01Attack XB01Attack {
        get {
            if (xb01Attack == null) {
                xb01Attack = BossAttack as XB01Attack;
            }
            return xb01Attack;
        }
    }

    private XB01Move xb01Move;
    public XB01Move XB01Move {
        get {
            if (xb01Move == null) {
                xb01Move = BossMove as XB01Move;
            }
            return xb01Move;
        }
    }

    private XB01Health xb01Health;
    public XB01Health XB01Health {
        get {
            if (xb01Health == null) {
                xb01Health = BossHealth as XB01Health;
            }
            return xb01Health;
        }
    }

    private XB01Stat xb01Stat;
    public XB01Stat XB01Stat {
        get {
            if (xb01Stat == null) {
                xb01Stat = BossStat as XB01Stat;
            }
            return xb01Stat;
        }
    }

    private XB01Hitbox xb01Hitbox;
    public XB01Hitbox XB01Hitbox {
        get {
            if (xb01Hitbox == null) {
                xb01Hitbox = BossHitbox as XB01Hitbox;
            }
            return xb01Hitbox;
        }
    }

    private XB01Skill xb01Skill;
    public XB01Skill XB01Skill {
        get {
            if (xb01Skill == null) {
                xb01Skill = BossSkill as XB01Skill;
            }
            return xb01Skill;
        }
    }
    #endregion
}
