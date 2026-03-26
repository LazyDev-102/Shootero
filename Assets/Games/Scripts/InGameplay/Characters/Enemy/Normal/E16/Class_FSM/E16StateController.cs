

using Class_FSM;

public class E16StateController : StateController<E16Base> {
    private E16Transition[] transitons = { E16HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E16Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E16StartState.Instance);
        E16StartState.Instance.StartState(this);
    }
}
