

using Class_FSM;

public class B09StateController : StateController<B09Base> {

    private B09Transition[] transitions = { B09IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B09Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B09StartState.Instance);
        B09StartState.Instance.StartState(this);
    }
}
