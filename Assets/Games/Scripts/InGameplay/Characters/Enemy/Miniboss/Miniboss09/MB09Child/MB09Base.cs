using UnityEngine;

[RequireComponent(typeof(MB09Attack), typeof(MB09Move), typeof(MB09Health))]
[RequireComponent(typeof(MB09Stat), typeof(MB09Hitbox), typeof(MB09Skill))]
[RequireComponent(typeof(MB09Effect), typeof(MB09StateController))]
public class MB09Base : MinibossBase {
    #region References
    private MB09Attack mb09Attack;
    public MB09Attack MB09Attack {
        get {
            if (mb09Attack == null) {
                mb09Attack = EnemyAttack as MB09Attack;
            }
            return mb09Attack;
        }
    }

    private MB09Move mb09Move;
    public MB09Move MB09Move {
        get {
            if (mb09Move == null) {
                mb09Move = EnemyMove as MB09Move;
            }
            return mb09Move;
        }
    }

    private MB09Health mb09Health;
    public MB09Health MB09Health {
        get {
            if (mb09Health == null) {
                mb09Health = EnemyHealth as MB09Health;
            }
            return mb09Health;
        }
    }

    private MB09Stat mb09Stat;
    public MB09Stat MB09Stat {
        get {
            if (mb09Stat == null) {
                mb09Stat = EnemyStat as MB09Stat;
            }
            return mb09Stat;
        }
    }

    private MB09Hitbox mb09Hitbox;
    public MB09Hitbox MB09Hitbox {
        get {
            if (mb09Hitbox == null) {
                mb09Hitbox = EnemyHitbox as MB09Hitbox;
            }
            return mb09Hitbox;
        }
    }

    private MB09Skill mb09Skill;
    public MB09Skill MB09Skill {
        get {
            if (mb09Skill == null) {
                mb09Skill = EnemySkill as MB09Skill;
            }
            return mb09Skill;
        }
    }

    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private float shieldTimeLife = 5;

    private MB09ParentBase myParent;
    private int currentPhase;
    private bool activeShield;
    private Countdowner shieldCD = new Countdowner();

    public MB09ParentBase MyParent { get => myParent; }
    public override void Initialize() {
        canDispatchMinibossSpawn = false;
        base.Initialize();
        currentPhase = 0;
        shieldCD.StartCountdown(shieldTimeLife);
    }
    public override void Updating() {
        base.Updating();

        if (currentPhase < phases.Length && MB09Health.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MB09Hitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            MB09Hitbox.TurnOnShield();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                MB09Hitbox.TurnOffShield();
            }
        }
    }
    public void SetParent(MB09ParentBase myParent) {
        this.myParent = myParent;
    }
    #endregion
}
