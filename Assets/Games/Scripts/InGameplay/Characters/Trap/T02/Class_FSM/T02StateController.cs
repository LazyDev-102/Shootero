

using Class_FSM;

public class T02StateController : StateController<T02Base> {
    protected override void DoAlwaysActions() {
    }

    protected override Transition<T02Base>[] GetTransitionFromAnyState() {
        return null;
    }

    protected override void StartStatrState() {
        SetCurrentState(T02StartState.Instance);
        T02StartState.Instance.StartState(this);
    }
}
