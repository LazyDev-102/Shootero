

using Class_FSM;

public class E10StateController : StateController<E10Base> {
    private E10Transition[] transitions = { E10IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E10Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(E10StartState.Instance);
        E10StartState.Instance.StartState(this);
    }
}
