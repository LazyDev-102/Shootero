

using Class_FSM;

public class B05StateController : StateController<B05Base> {

    private B05Transition[] transitions = { B05IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B05Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B05StartState.Instance);
        B05StartState.Instance.StartState(this);
    }
}
