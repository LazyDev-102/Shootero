

using Class_FSM;

public class E17StateController : StateController<E17Base> {
    private E17Transition[] transitons = { E17HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E17Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E17StartState.Instance);
        E17StartState.Instance.StartState(this);
    }
}
