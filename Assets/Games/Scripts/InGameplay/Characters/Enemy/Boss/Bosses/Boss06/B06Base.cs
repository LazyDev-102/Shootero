using UnityEngine;
[RequireComponent(typeof(B06Attack), typeof(B06Health), typeof(B06Move))]
[RequireComponent(typeof(B06Skill), typeof(B06Stat), typeof(B06HitBox))]
public class B06Base : BossBase {
    #region References
    private B06Attack b06Attack;
    public B06Attack B06Attack {
        get {
            if(b06Attack == null) {
                b06Attack = BossAttack as B06Attack;
            }
            return b06Attack;
        }
    }

    private B06Move b06Move;
    public B06Move B06Move {
        get {
            if(b06Move == null) {
                b06Move = BossMove as B06Move;
            }
            return b06Move;
        }
    }

    private B06Health b06Health;
    public B06Health B06Health {
        get {
            if(b06Health == null) {
                b06Health = BossHealth as B06Health;
            }
            return b06Health;
        }
    }

    private B06Stat b06Stat;
    public B06Stat B06Stat {
        get {
            if(b06Stat == null) {
                b06Stat = BossStat as B06Stat;
            }
            return b06Stat;
        }
    }

    private B06HitBox b06Hitbox;
    public B06HitBox B06Hitbox {
        get {
            if(b06Hitbox == null) {
                b06Hitbox = BossHitbox as B06HitBox;
            }
            return b06Hitbox;
        }
    }

    private B06Skill b06Skill;
    public B06Skill B06Skill {
        get {
            if(b06Skill == null) {
                b06Skill = BossSkill as B06Skill;
            }
            return b06Skill;
        }
    }
    #endregion
}
