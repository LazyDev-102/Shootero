

using Class_FSM;

public class E08StateController : StateController<E08Base> {
    private E08Transition[] transitions = { E08IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E08Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(E08StartState.Instance);
        E08StartState.Instance.StartState(this);
    }
}
