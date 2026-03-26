

using Class_FSM;

public class B14StateController : StateController<B14Base> {

    private B14Transition[] transitions = { B14IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B14Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B14StartState.Instance);
        B14StartState.Instance.StartState(this);
    }
}
