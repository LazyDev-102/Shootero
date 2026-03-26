

using Class_FSM;

public class B10StateController : StateController<B10Base> {
    private B10Transition[] transitions = { B10IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B10Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B10StartState.Instance);
        B10StartState.Instance.StartState(this);
    }
}
