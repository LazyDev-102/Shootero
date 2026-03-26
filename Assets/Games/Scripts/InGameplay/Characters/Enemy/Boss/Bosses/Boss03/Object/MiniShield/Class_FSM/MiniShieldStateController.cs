

using Class_FSM;

public class MiniShieldStateController : StateController<MiniShieldBase> {

    protected override void DoAlwaysActions() {
    }

    protected override Transition<MiniShieldBase>[] GetTransitionFromAnyState() {
        return null;
    }

    protected override void StartStatrState() {
        SetCurrentState(MiniShieldIdleState.Instance);
        MiniShieldIdleState.Instance.StartState(this);
    }
}
