

using Class_FSM;

public class B06StateController : StateController<B06Base> {

    private B06Transition[] transitions = { B06IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B06Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B06StartState.Instance);
        B06StartState.Instance.StartState(this);
    }
}
