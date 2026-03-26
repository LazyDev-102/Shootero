

using Class_FSM;

public class ME03B08StateController : StateController<ME03B08Base> {
    private ME03B08Transition[] transitions = { ME03B08IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<ME03B08Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(ME03B08StartState.Instance);
        ME03B08StartState.Instance.StartState(this);
    }
}
