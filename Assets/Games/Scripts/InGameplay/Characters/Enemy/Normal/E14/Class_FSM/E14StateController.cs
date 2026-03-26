

using Class_FSM;

public class E14StateController : StateController<E14Base> {
    private E14Transition[] transitons = { E14HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E14Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E14StartState.Instance);
        E14StartState.Instance.StartState(this);
    }
}
