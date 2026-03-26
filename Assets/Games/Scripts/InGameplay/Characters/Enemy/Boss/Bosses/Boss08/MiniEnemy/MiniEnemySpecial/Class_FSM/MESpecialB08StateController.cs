

using Class_FSM;

public class MESpecialB08StateController : StateController<MESpecialB08Base> {
    private MESpecialB08Transition[] transitions = { MESpecialB08IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MESpecialB08Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MESpecialB08IdleState.Instance);
        MESpecialB08IdleState.Instance.StartState(this);
    }
}
