using Class_FSM;

public class MB08StateController : StateController<MB08Base> {
    private MB08Transition[] transitions = { MB08IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB08Base>[] GetTransitionFromAnyState() {
        return transitions;

    }

    protected override void StartStatrState() {
        SetCurrentState(MB08StartState.Instance);
        MB08StartState.Instance.StartState(this);
    }
}
