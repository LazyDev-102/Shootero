using Class_FSM;
using UnityEngine;

public class MB15ChildStateController : StateController<MB15ChildBase> {
    private MB15ChildTransition[] transitions = { MB15ChildIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB15ChildBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB15ChildIdleState.Instance);
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
        //MB15ChildStartState.Instance.StartState(this);
    }
}
