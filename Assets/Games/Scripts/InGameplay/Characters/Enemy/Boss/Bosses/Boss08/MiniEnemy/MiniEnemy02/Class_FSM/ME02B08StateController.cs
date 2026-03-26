

using Class_FSM;

public class ME02B08StateController : StateController<ME02B08Base> {
    private ME02B08Transition[] transitions = { ME02B08IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<ME02B08Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(ME02B08StartState.Instance);
        ME02B08StartState.Instance.StartState(this);
    }
}
