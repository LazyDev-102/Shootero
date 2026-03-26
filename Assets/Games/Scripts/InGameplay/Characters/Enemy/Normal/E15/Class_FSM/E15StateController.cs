

using Class_FSM;

public class E15StateController : StateController<E15Base> {
    private E15Transition[] transitons = { E15HasDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<E15Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(E15StartState.Instance);
        E15StartState.Instance.StartState(this);
    }
}
