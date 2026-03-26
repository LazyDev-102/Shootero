

using Class_FSM;

public class B02StateController : StateController<B02Base> {
    private B02Transition[] transitions = { B02IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B02Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B02StartState.Instance);
        B02StartState.Instance.StartState(this);
    }
}
