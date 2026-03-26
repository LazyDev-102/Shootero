

using Class_FSM;

public class T04StateController : StateController<T04Base> {

    protected override void DoAlwaysActions() {
    }

    protected override Transition<T04Base>[] GetTransitionFromAnyState() {
        return null;
    }

    protected override void StartStatrState() {
        SetCurrentState(T04StartState.Instance);
        T04StartState.Instance.StartState(this);
    }
}
