

using Class_FSM;

public class B12ChildStateController : StateController<B12ChildBase> {
    private Transition<B12ChildBase>[] transitions = { B12ChildCanDeadTransition.Instance };

    protected override void StartStatrState() {
        SetCurrentState(B12ChildIdleState.Instance);
        B12ChildIdleState.Instance.StartState(this);
    }
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B12ChildBase>[] GetTransitionFromAnyState() {
        return transitions;
    }
}
