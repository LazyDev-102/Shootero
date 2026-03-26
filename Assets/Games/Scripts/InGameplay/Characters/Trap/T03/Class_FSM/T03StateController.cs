

using Class_FSM;

public class T03StateController : StateController<T03Base> {

    protected override void DoAlwaysActions() {
    }

    protected override Transition<T03Base>[] GetTransitionFromAnyState() {
        return null;
    }

    protected override void StartStatrState() {
        SetCurrentState(T03StartState.Instance);
        T03StartState.Instance.StartState(this);
    }
}
