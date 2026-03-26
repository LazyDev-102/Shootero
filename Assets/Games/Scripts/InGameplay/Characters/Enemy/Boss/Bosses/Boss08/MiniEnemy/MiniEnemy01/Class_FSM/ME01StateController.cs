

using Class_FSM;

public class ME01StateController : StateController<ME01Base> {
    private ME01Transition[] transitions = { ME01IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<ME01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(ME01StartState.Instance);
        ME01StartState.Instance.StartState(this);
    }
}
