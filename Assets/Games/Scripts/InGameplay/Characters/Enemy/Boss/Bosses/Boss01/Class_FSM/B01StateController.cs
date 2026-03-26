

using Class_FSM;

public class B01StateController : StateController<B01Base> {

    private B01Transition[] transitions = { B01IsDieTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<B01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B01StartState.Instance);
        B01StartState.Instance.StartState(this);
    }
}
