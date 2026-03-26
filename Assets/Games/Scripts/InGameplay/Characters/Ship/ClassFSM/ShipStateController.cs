

using Class_FSM;

public class ShipStateController : StateController<ShipBase> {
    private ShipTransition[] transitions = { ToDieShipTransition.Instance };
    protected override void StartStatrState() {
        SetCurrentState(ShipAppearState.Instance);
        ShipAppearState.Instance.StartState(this);
    }
    protected override void DoAlwaysActions() {

    }

    protected override Transition<ShipBase>[] GetTransitionFromAnyState() {
        return transitions;
    }
}
