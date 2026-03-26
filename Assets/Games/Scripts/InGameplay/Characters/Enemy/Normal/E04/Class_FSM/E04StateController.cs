

using Class_FSM;

public class E04StateController : StateController<E04Base> {
    private E04Transition[] transitions = { E04HasDieTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<E04Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(E04StartState.Instance);
        E04StartState.Instance.StartState(this);
    }
}
