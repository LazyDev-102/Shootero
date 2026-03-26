

using Class_FSM;

public class B07StateController : StateController<B07Base> {
    private B07Transition[] transitions = { B07IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B07Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B07StartState.Instance);
        B07StartState.Instance.StartState(this);
    }
}
