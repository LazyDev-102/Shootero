using UnityEngine;

[RequireComponent(typeof(MB07Attack), typeof(MB07Move), typeof(MB07Health))]
[RequireComponent(typeof(MB07Stat), typeof(MB07Hitbox), typeof(MB07Skill))]
[RequireComponent(typeof(MB07Effect), typeof(MB07StateController))]
public class MB07Base : MinibossBase {
    #region References
    private MB07Attack mb07Attack;
    public MB07Attack MB07Attack {
        get {
            if (mb07Attack == null) {
                mb07Attack = EnemyAttack as MB07Attack;
            }
            return mb07Attack;
        }
    }

    private MB07Move mb07Move;
    public MB07Move MB07Move {
        get {
            if (mb07Move == null) {
                mb07Move = EnemyMove as MB07Move;
            }
            return mb07Move;
        }
    }

    private MB07Health mb07Health;
    public MB07Health MB07Health {
        get {
            if (mb07Health == null) {
                mb07Health = EnemyHealth as MB07Health;
            }
            return mb07Health;
        }
    }

    private MB07Stat mb07Stat;
    public MB07Stat MB07Stat {
        get {
            if (mb07Stat == null) {
                mb07Stat = EnemyStat as MB07Stat;
            }
            return mb07Stat;
        }
    }

    private MB07Hitbox mb07Hitbox;
    public MB07Hitbox MB07Hitbox {
        get {
            if (mb07Hitbox == null) {
                mb07Hitbox = EnemyHitbox as MB07Hitbox;
            }
            return mb07Hitbox;
        }
    }

    private MB07Skill mb07Skill;
    public MB07Skill MB07Skill {
        get {
            if (mb07Skill == null) {
                mb07Skill = EnemySkill as MB07Skill;
            }
            return mb07Skill;
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

        if (currentPhase < phases.Length && MB07Health.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MB07Hitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            MB07Hitbox.TurnOnShield();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                MB07Hitbox.TurnOffShield();
            }
        }
    }

    #endregion
}
