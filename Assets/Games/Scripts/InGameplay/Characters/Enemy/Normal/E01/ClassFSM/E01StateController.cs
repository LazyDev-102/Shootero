using Class_FSM;

public class E01StateController : StateController<E01Base> {
    private Transition<E01Base>[] transitions = { E01CanDieTransition.Instance };
    protected override void StartStatrState()
    {
        SetCurrentState(E01IdleState.Instance);
        E01IdleState.Instance.StartState(this);
    }
    protected override void DoAlwaysActions()
    {

    }

    protected override Transition<E01Base>[] GetTransitionFromAnyState()
    {
        return transitions;
    }
}
