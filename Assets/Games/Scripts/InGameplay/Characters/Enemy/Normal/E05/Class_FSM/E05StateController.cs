

using Class_FSM;

public class E05StateController : StateController<E05Base> {
    private E05Transition[] transitons = { E05HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E05Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E05StartState.Instance);
        E05StartState.Instance.StartState(this);
    }
}
