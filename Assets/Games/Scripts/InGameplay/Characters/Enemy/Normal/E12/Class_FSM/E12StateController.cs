

using Class_FSM;

public class E12StateController : StateController<E12Base> {
    private E12Transition[] transitons = { E12HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E12Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E12StartState.Instance);
        E12StartState.Instance.StartState(this);
    }
}
