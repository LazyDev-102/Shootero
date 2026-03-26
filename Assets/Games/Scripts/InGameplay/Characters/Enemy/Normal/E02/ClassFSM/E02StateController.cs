

using Class_FSM;

public class E02StateController : StateController<E02Base> {
    private Transition<E02Base>[] transitions = { E02CanDeadTransition.Instance };

    protected override void StartStatrState() {
        SetCurrentState(E02IdleState.Instance);
        E02IdleState.Instance.StartState(this);
    }
    protected override void DoAlwaysActions() {

    }

    protected override Transition<E02Base>[] GetTransitionFromAnyState() {
        return transitions;
    }
}
