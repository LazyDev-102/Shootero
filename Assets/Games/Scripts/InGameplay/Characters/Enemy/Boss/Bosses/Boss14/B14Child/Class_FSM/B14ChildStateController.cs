using Class_FSM;
using UnityEngine;

public class B14ChildStateController : StateController<B14ChildBase> {
    private B14ChildTransition[] transitions = { B14ChildIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<B14ChildBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(B14ChildIdleState.Instance);
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
        //B14ChildStartState.Instance.StartState(this);
    }
}
