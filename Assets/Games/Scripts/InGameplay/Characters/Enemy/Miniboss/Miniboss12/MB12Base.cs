using UnityEngine;

[RequireComponent(typeof(MB12Attack), typeof(MB12Move), typeof(MB12Health))]
[RequireComponent(typeof(MB12Stat), typeof(MB12Hitbox), typeof(MB12Skill))]
[RequireComponent(typeof(MB12Effect), typeof(MB12StateController))]
public class MB12Base : MinibossBase {
    #region References
    private MB12Attack mb12Attack;
    public MB12Attack MB12Attack {
        get {
            if (mb12Attack == null) {
                mb12Attack = EnemyAttack as MB12Attack;
            }
            return mb12Attack;
        }
    }

    private MB12Move mb12Move;
    public MB12Move MB12Move {
        get {
            if (mb12Move == null) {
                mb12Move = EnemyMove as MB12Move;
            }
            return mb12Move;
        }
    }

    private MB12Health mb12Health;
    public MB12Health MB12Health {
        get {
            if (mb12Health == null) {
                mb12Health = EnemyHealth as MB12Health;
            }
            return mb12Health;
        }
    }

    private MB12Stat mb12Stat;
    public MB12Stat MB12Stat {
        get {
            if (mb12Stat == null) {
                mb12Stat = EnemyStat as MB12Stat;
            }
            return mb12Stat;
        }
    }

    private MB12Hitbox mb12Hitbox;
    public MB12Hitbox MB12Hitbox {
        get {
            if (mb12Hitbox == null) {
                mb12Hitbox = EnemyHitbox as MB12Hitbox;
            }
            return mb12Hitbox;
        }
    }

    private MB12Skill mb12Skill;
    public MB12Skill MB12Skill {
        get {
            if (mb12Skill == null) {
                mb12Skill = EnemySkill as MB12Skill;
            }
            return mb12Skill;
        }
    }
    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private Shield shield;
    [SerializeField] private float shieldTimeLife = 10;

    private int currentPhase;
    private bool activeShield;
    private Countdowner shieldCD = new Countdowner();

    public override void Initialize() {
        base.Initialize();
        currentPhase = 0;
        shieldCD.StartCountdown(shieldTimeLife);
    }
    public override void Updating() {
        base.Updating();

        if (currentPhase < phases.Length && MB12Health.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MB12Hitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            shield.TurnOn();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                shield.TurnOff();
            }
        }
    }

    #endregion
}
