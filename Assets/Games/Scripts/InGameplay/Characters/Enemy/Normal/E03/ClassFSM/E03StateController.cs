using Class_FSM;

public class E03StateController : StateController<E03Base> {
    private E03Transition[] transitions = { E03HasDieTransition.Instance};
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E03Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(E03IdleState.Instance);
        E03IdleState.Instance.StartState(this);
    }
}
