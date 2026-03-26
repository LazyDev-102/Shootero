

using Class_FSM;

public class E09StateController : StateController<E09Base> {
    private E09Transition[] transitions = { E09IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E09Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(E09StartState.Instance);
        E09StartState.Instance.StartState(this);
    }
}
