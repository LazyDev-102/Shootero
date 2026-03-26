

using Class_FSM;

public class HB01StateController : StateController<HB01Base> {

    private HB01Transition[] transitions = { HB01IsDieTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<HB01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(HB01StartState.Instance);
        HB01StartState.Instance.StartState(this);
    }
}
