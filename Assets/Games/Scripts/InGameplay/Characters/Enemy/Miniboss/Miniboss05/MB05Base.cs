using UnityEngine;

[RequireComponent(typeof(MB05Attack), typeof(MB05Move), typeof(MB05Health))]
[RequireComponent(typeof(MB05Stat), typeof(MB05Hitbox), typeof(MB05Skill))]
[RequireComponent(typeof(MB05Effect), typeof(MB05StateController))]
public class MB05Base : MinibossBase {
    #region References
    private MB05Attack mb05Attack;
    public MB05Attack MB05Attack {
        get {
            if (mb05Attack == null) {
                mb05Attack = EnemyAttack as MB05Attack;
            }
            return mb05Attack;
        }
    }

    private MB05Move mb05Move;
    public MB05Move MB05Move {
        get {
            if (mb05Move == null) {
                mb05Move = EnemyMove as MB05Move;
            }
            return mb05Move;
        }
    }

    private MB05Health mb05Health;
    public MB05Health MB05Health {
        get {
            if (mb05Health == null) {
                mb05Health = EnemyHealth as MB05Health;
            }
            return mb05Health;
        }
    }

    private MB05Stat mb05Stat;
    public MB05Stat MB05Stat {
        get {
            if (mb05Stat == null) {
                mb05Stat = EnemyStat as MB05Stat;
            }
            return mb05Stat;
        }
    }

    private MB05Hitbox mb05Hitbox;
    public MB05Hitbox MB05Hitbox {
        get {
            if (mb05Hitbox == null) {
                mb05Hitbox = EnemyHitbox as MB05Hitbox;
            }
            return mb05Hitbox;
        }
    }

    private MB05Skill mb05Skill;
    public MB05Skill MB05Skill {
        get {
            if (mb05Skill == null) {
                mb05Skill = EnemySkill as MB05Skill;
            }
            return mb05Skill;
        }
    }
    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
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

        if (currentPhase < phases.Length && MB05Health.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MB05Hitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            MB05Hitbox.TurnOnShield();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                MB05Hitbox.TurnOffShield();
            }
        }
    }

    #endregion
}
