

using Helper;
using UnityEngine;

public class B04Base : BossBase {
    #region References
    private B04Attack b04Attack;
    public B04Attack B04Attack {
        get {
            if (b04Attack == null) {
                b04Attack = BossAttack as B04Attack;
            }
            return b04Attack;
        }
    }

    private B04Move b04Move;
    public B04Move B04Move {
        get {
            if (b04Move == null) {
                b04Move = BossMove as B04Move;
            }
            return b04Move;
        }
    }

    private B04Health b04Health;
    public B04Health B04Health {
        get {
            if (b04Health == null) {
                b04Health = BossHealth as B04Health;
            }
            return b04Health;
        }
    }

    private B04Stat b04Stat;
    public B04Stat B04Stat {
        get {
            if (b04Stat == null) {
                b04Stat = BossStat as B04Stat;
            }
            return b04Stat;
        }
    }

    private B04Hitbox b04Hitbox;
    public B04Hitbox B04Hitbox {
        get {
            if (b04Hitbox == null) {
                b04Hitbox = BossHitbox as B04Hitbox;
            }
            return b04Hitbox;
        }
    }

    private B04Skill b04Skill;
    public B04Skill B04Skill {
        get {
            if (b04Skill == null) {
                b04Skill = BossSkill as B04Skill;
            }
            return b04Skill;
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
        B04Move.LookDirection(UnityHelper.Down);
        //B04Move.ClosingWing();
    }

    public bool CanMoveRage() {
        return delayMoveRageCountdowner.IsTimeOut();
    }
}
