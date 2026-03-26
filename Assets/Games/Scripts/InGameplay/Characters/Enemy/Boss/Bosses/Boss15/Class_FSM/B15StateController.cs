

using Class_FSM;

public class B15StateController : StateController<B15Base> {

    private B15Transition[] transitions = { B15IsDieTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B15Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B15StartState.Instance);
        B15StartState.Instance.StartState(this);
    }
}
