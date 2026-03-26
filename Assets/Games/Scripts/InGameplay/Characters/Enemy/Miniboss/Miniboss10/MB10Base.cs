using UnityEngine;

[RequireComponent(typeof(MB10Attack), typeof(MB10Move), typeof(MB10Health))]
[RequireComponent(typeof(MB10Stat), typeof(MB10Hitbox), typeof(MB10Skill))]
[RequireComponent(typeof(MB10Effect), typeof(MB10StateController))]
public class MB10Base : MinibossBase {
    #region References
    private MB10Attack mb10Attack;
    public MB10Attack MB10Attack {
        get {
            if (mb10Attack == null) {
                mb10Attack = EnemyAttack as MB10Attack;
            }
            return mb10Attack;
        }
    }

    private MB10Move mb10Move;
    public MB10Move MB10Move {
        get {
            if (mb10Move == null) {
                mb10Move = EnemyMove as MB10Move;
            }
            return mb10Move;
        }
    }

    private MB10Health mb10Health;
    public MB10Health MB10Health {
        get {
            if (mb10Health == null) {
                mb10Health = EnemyHealth as MB10Health;
            }
            return mb10Health;
        }
    }

    private MB10Stat mb10Stat;
    public MB10Stat MB10Stat {
        get {
            if (mb10Stat == null) {
                mb10Stat = EnemyStat as MB10Stat;
            }
            return mb10Stat;
        }
    }

    private MB10Hitbox mb10Hitbox;
    public MB10Hitbox MB10Hitbox {
        get {
            if (mb10Hitbox == null) {
                mb10Hitbox = EnemyHitbox as MB10Hitbox;
            }
            return mb10Hitbox;
        }
    }

    private MB10Skill mb10Skill;
    public MB10Skill MB10Skill {
        get {
            if (mb10Skill == null) {
                mb10Skill = EnemySkill as MB10Skill;
            }
            return mb10Skill;
        }
    }
    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private Shield shield;
    [SerializeField] private float shieldTimeLife = 5;

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

        if (currentPhase < phases.Length && MB10Health.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MB10Hitbox.TurnOnInvulnerable(shieldTimeLife + 1);
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
