

public class ShieldEffect : ShipSeflEffect {
    public static string shiledId = "shield";
    private FloatStat shieldDurantion;
    private FloatStat shieldCountdown;

    private Countdowner shieldDurantionCountdowner = new Countdowner();
    private Countdowner shieldCountdownCountdowner = new Countdowner();
    private bool pauseEffect;
    public ShieldEffect(ShipBase ship, float shieldDurantion, float shieldCountdown) : base(ship) {
        id = shiledId;
        this.shieldDurantion = new FloatStat(shieldDurantion);
        this.shieldCountdown = new FloatStat(shieldCountdown);
    }

    public FloatStat ShieldDurantion { get => shieldDurantion; }
    public FloatStat ShieldCountdown { get => shieldCountdown; }

    public override void EffectTo() {
        shieldDurantionCountdowner.StartCountdown(shieldDurantion.Value);
        ship.ShipHitbox.TurnOnProtectShield();

    }

    public override void Updating(float deltaTime) {
        if (pauseEffect)
            return;
        if (shieldCountdownCountdowner.IsTimeOut()) {
            if (shieldDurantionCountdowner.IsCountdowning()) {
                shieldDurantionCountdowner.Countdowning(deltaTime);
                //ship.ShipHitbox.RotateShield();
                if (shieldDurantionCountdowner.IsTimeOut()) {
                    ship.ShipHitbox.TurnOffProtectShield();
                    shieldCountdownCountdowner.StartCountdown(shieldCountdown.Value);
                }
            }
        }
        else {
            shieldCountdownCountdowner.Countdowning(deltaTime);
            if (shieldCountdownCountdowner.IsTimeOut()) {
                ship.ShipHitbox.TurnOnProtectShield();
                shieldDurantionCountdowner.StartCountdown(shieldDurantion.Value);
            }
        }
    }

    protected override void RemoveFrom() {
        ship.ShipSkill.RemoveSelfEffect(this);
    }
    public void PauseEffect(bool status) {
        pauseEffect = true;
    }
}
