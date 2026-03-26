

using Class_FSM;

public class E07StateController : StateController<E07Base> {
    private E07Transition[] transitions = { E07IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E07Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(E07StartState.Instance);
        E07StartState.Instance.StartState(this);
    }
}
