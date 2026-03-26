public class EnergyShieldEffect : ShipSeflEffect {
    public static string shieldId = "energyShield";
    private IntStat hp;
    private FloatStat dodgeRate;
    private FloatStat timeReborn;

    public EnergyShieldEffect(ShipBase ship, int hp, float dodgeRate, float shieldCountdown) : base(ship) {
        id = shieldId;
        this.hp = new IntStat(hp);
        this.dodgeRate = new FloatStat(dodgeRate);
        this.timeReborn = new FloatStat(shieldCountdown);
    }

    public IntStat HP { get => hp; }
    public FloatStat TimeReborn { get => timeReborn; }
    public FloatStat DodgeRate { get => dodgeRate; }

    public override void EffectTo() {
        ship.ShipHitbox.TurnOnShield(false, hp.Value, dodgeRate.Value, timeReborn.Value);
    }

    public override void Updating(float deltaTime) {

    }

    protected override void RemoveFrom() {
        ship.ShipSkill.RemoveSelfEffect(this);
    }
    public void PauseEffect(bool status) {
        ship.ShipHitbox.TurnOffShield(false);
    }
}
