

using Class_FSM;

public class B08StateTransition : StateController<B08Base> {
    private B08Transition[] transitions = { B08IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B08Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B08StartState.Instance);
        B08StartState.Instance.StartState(this);
    }
}
