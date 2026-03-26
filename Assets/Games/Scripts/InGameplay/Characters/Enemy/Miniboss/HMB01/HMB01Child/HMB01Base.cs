using UnityEngine;

[RequireComponent(typeof(HMB01Attack), typeof(HMB01Move), typeof(MinibossHealth))]
[RequireComponent(typeof(MinibossStat), typeof(MinibossHitbox), typeof(MinibossSkill))]
[RequireComponent(typeof(MinibossEffect), typeof(HMB01StateController))]
public class HMB01Base : MinibossBase {
    #region References
    private HMB01Attack mb09Attack;
    public HMB01Attack HMB01Attack {
        get {
            if (mb09Attack == null) {
                mb09Attack = EnemyAttack as HMB01Attack;
            }
            return mb09Attack;
        }
    }

    private HMB01Move mb09Move;
    public HMB01Move HMB01Move {
        get {
            if (mb09Move == null) {
                mb09Move = EnemyMove as HMB01Move;
            }
            return mb09Move;
        }
    }

    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private float shieldTimeLife = 5;

    private HMB01ParentBase myParent;
    private int currentPhase;
    private bool activeShield;
    private Countdowner shieldCD = new Countdowner();

    public HMB01ParentBase MyParent { get => myParent; }
    public override void Initialize() {
        canDispatchMinibossSpawn = false;
        base.Initialize();
        currentPhase = 0;
        shieldCD.StartCountdown(shieldTimeLife);
    }
    public override void Updating() {
        base.Updating();

        if (currentPhase < phases.Length && MinibossHealth.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MinibossHitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            MinibossHitbox.TurnOnShield();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                MinibossHitbox.TurnOffShield();
            }
        }
    }
    public void SetParent(HMB01ParentBase myParent) {
        this.myParent = myParent;
    }
    #endregion
}
