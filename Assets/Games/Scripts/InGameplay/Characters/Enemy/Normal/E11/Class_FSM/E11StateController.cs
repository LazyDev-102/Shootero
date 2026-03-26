

using Class_FSM;

public class E11StateController : StateController<E11Base> {
    private E11Transition[] transitons = { E11HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E11Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E11StartState.Instance);
        E11StartState.Instance.StartState(this);
    }
}
