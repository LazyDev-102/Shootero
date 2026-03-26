

using Class_FSM;

public class B11StateController : StateController<B11Base> {

    private B11Transition[] transitions = { B11IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B11Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B11StartState.Instance);
        B11StartState.Instance.StartState(this);
    }
}
