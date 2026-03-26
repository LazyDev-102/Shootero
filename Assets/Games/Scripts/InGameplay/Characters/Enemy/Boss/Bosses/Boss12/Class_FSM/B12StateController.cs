

using Class_FSM;

public class B12StateController : StateController<B12Base> {

    private B12Transition[] transitions = { B12IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B12Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B12StartState.Instance);
        B12StartState.Instance.StartState(this);
    }
}
