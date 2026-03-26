

public abstract class ShipSeflEffect {
    protected string id;
    protected ShipBase ship;

    protected ShipSeflEffect(ShipBase ship) {
        this.ship = ship;
    }

    public string Id { get => id; }

    public abstract void EffectTo();
    protected abstract void RemoveFrom();

    public override bool Equals(object other) {
        if (other == null)
            return false;
        ShipSeflEffect effectOther = other as ShipSeflEffect;
        return this.id.Equals(effectOther.id);
    }

    public abstract void Updating(float deltaTime);

    public override int GetHashCode() {
        return this.id.GetHashCode();
    }
}
