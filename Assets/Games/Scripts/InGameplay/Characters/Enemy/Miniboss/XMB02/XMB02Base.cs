using UnityEngine;

[RequireComponent(typeof(XMB02Attack), typeof(XMB02Move), typeof(MinibossHealth))]
[RequireComponent(typeof(MinibossStat), typeof(MinibossHitbox), typeof(MinibossSkill))]
[RequireComponent(typeof(MinibossEffect), typeof(XMB02StateController))]
public class XMB02Base : MinibossBase {
    #region References
    private XMB02Attack mb10Attack;
    public XMB02Attack XMB02Attack {
        get {
            if (mb10Attack == null) {
                mb10Attack = EnemyAttack as XMB02Attack;
            }
            return mb10Attack;
        }
    }

    private XMB02Move mb10Move;
    public XMB02Move XMB02Move {
        get {
            if (mb10Move == null) {
                mb10Move = EnemyMove as XMB02Move;
            }
            return mb10Move;
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

        if (currentPhase < phases.Length && MinibossHealth.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MinibossHitbox.TurnOnInvulnerable(shieldTimeLife + 1);
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
