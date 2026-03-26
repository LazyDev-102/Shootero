using UnityEngine;

[RequireComponent(typeof(XMB01Attack), typeof(XMB01Move), typeof(MinibossHealth))]
[RequireComponent(typeof(MinibossStat), typeof(MinibossHitbox), typeof(MinibossSkill))]
[RequireComponent(typeof(MinibossEffect), typeof(XMB01StateController))]
public class XMB01Base : MinibossBase {
    #region References
    private XMB01Attack mb09Attack;
    public XMB01Attack XMB01Attack {
        get {
            if (mb09Attack == null) {
                mb09Attack = EnemyAttack as XMB01Attack;
            }
            return mb09Attack;
        }
    }

    private XMB01Move mb09Move;
    public XMB01Move XMB01Move {
        get {
            if (mb09Move == null) {
                mb09Move = EnemyMove as XMB01Move;
            }
            return mb09Move;
        }
    }

    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private float shieldTimeLife = 5;

    private XMB01ParentBase myParent;
    private int currentPhase;
    private bool activeShield;
    private Countdowner shieldCD = new Countdowner();

    public XMB01ParentBase MyParent { get => myParent; }
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
    public void SetParent(XMB01ParentBase myParent) {
        this.myParent = myParent;
    }
    #endregion
}
