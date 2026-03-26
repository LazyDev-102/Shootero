

using Class_FSM;

public class B03StateController : StateController<B03Base> {
    private B03Transition[] transitions = { B03CanDeadTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B03Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B03StartState.Instance);
        B03StartState.Instance.StartState(this);
    }
}
