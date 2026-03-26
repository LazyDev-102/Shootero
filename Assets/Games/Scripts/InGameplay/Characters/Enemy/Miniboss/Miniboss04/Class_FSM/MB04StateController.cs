using Class_FSM;

public class MB04StateController : StateController<MB04Base> {
    private MB04Transition[] transitions = { MB04IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB04Base>[] GetTransitionFromAnyState() {
        return transitions;

    }

    protected override void StartStatrState() {
        SetCurrentState(MB04StartState.Instance);
        MB04StartState.Instance.StartState(this);
    }
}
