

using Class_FSM;

public class T01StateController : StateController<T01Base> {
    protected override void DoAlwaysActions() {
    }

    protected override Transition<T01Base>[] GetTransitionFromAnyState() {
        return null;
    }

    protected override void StartStatrState() {
        SetCurrentState(T01StartState.Instance);
        T01StartState.Instance.StartState(this);
    }
}
