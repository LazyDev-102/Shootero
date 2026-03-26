

using Class_FSM;

public class E06StateController : StateController<E06Base> {
    private E06Transition[] transitons = { E06HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E06Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E06StartState.Instance);
        E06StartState.Instance.StartState(this);
    }
}
