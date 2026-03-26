

using Class_FSM;

public class E13StateController : StateController<E13Base> {
    private E13Transition[] transitons = { E13HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E13Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E13StartState.Instance);
        E13StartState.Instance.StartState(this);
    }
}
