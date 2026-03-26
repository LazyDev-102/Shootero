

using Class_FSM;

public class XB01StateController : StateController<XB01Base> {

    private XB01Transition[] transitions = { XB01IsDieTransition.Instance };
    protected override void DoAlwaysActions() {

    }

    protected override Transition<XB01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(XB01StartState.Instance);
        XB01StartState.Instance.StartState(this);
    }
}
