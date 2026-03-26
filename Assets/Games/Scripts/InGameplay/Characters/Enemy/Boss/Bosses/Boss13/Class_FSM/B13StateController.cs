

using Class_FSM;

public class B13StateController : StateController<B13Base> {

    private B13Transition[] transitions = { B13IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B13Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B13StartState.Instance);
        B13StartState.Instance.StartState(this);
    }
}
