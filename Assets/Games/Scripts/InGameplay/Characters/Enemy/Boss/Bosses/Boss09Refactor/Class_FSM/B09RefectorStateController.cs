

using Class_FSM;

public class B09RefectorStateController : StateController<B09RefectorBase> {

    private B09RefectorTransition[] transitions = { B09RefectorIsDieTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B09RefectorBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B09RefectorStartState.Instance);
        B09RefectorStartState.Instance.StartState(this);
    }
}
