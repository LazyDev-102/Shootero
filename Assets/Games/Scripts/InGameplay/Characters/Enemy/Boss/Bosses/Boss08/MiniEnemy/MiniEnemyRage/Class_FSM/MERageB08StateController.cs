

using Class_FSM;

public class MERageB08StateController : StateController<MERageB08Base> {
    private MERageB08Transition[] transitions = { MERageB08IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MERageB08Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MERageB08AttackState.Instance);
        MERageB08AttackState.Instance.StartState(this);
    }
}
