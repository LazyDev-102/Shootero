using Helper;
using UnityEngine;


public class B02Base : BossBase {
    #region References
    private B02Attack b02Attack;
    public B02Attack B02Attack {
        get {
            if(b02Attack == null) {
                b02Attack = BossAttack as B02Attack;
            }
            return b02Attack;
        }
    }

    private B02Move b02Move;
    public B02Move B02Move {
        get {
            if(b02Move == null) {
                b02Move = BossMove as B02Move;
            }
            return b02Move;
        }
    }

    private B02Health b02Health;
    public B02Health B02Health {
        get {
            if(b02Health == null) {
                b02Health = BossHealth as B02Health;
            }
            return b02Health;
        }
    }

    private B02Stat b02Stat;
    public B02Stat B02Stat {
        get {
            if(b02Stat == null) {
                b02Stat = BossStat as B02Stat;
            }
            return b02Stat;
        }
    }

    private B02Hitbox b02Hitbox;
    public B02Hitbox B02Hitbox {
        get {
            if(b02Hitbox == null) {
                b02Hitbox = BossHitbox as B02Hitbox;
            }
            return b02Hitbox;
        }
    }

    private B02Skill b02Skill;
    public B02Skill B02Skill {
        get {
            if(b02Skill == null) {
                b02Skill = BossSkill as B02Skill;
            }
            return b02Skill;
        }
    }
    #endregion
    [SerializeField] private float delayMoveRage;

    private Countdowner delayMoveRageCountdowner = new Countdowner();

    public void StartLookDown() {
        delayMoveRageCountdowner.StartCountdown(delayMoveRage);
    }

    public void LookingDown() {
        delayMoveRageCountdowner.Countdowning(Time.deltaTime);
        B02Move.LookDirection(UnityHelper.Down);
    }

    public bool CanMoveRage() {
        return delayMoveRageCountdowner.IsTimeOut();
    }
}
