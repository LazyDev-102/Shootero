

using Class_FSM;

public class B04StateController : StateController<B04Base> {
    private B04Transition[] transitions = { B04IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B04Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B04StartState.Instance);
        B04StartState.Instance.StartState(this);
    }
}
