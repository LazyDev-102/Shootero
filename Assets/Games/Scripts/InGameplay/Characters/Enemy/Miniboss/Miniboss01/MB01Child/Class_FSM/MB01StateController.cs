using Class_FSM;
using UnityEngine;

public class MB01StateController : StateController<MB01Base> {
    private MB01Transition[] transitions = { MB01IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }
    protected override void StartStatrState() {
        SetCurrentState(MB01StartState.Instance);
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
        //MB01StartState.Instance.StartState(this);
    }
}
