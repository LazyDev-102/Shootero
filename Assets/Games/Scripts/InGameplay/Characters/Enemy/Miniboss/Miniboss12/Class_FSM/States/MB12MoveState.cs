using Class_FSM;
using UnityEngine;

public class MB12MoveState : MB12State {

    #region Singleton
    public MB12MoveState() {

    }
    private static MB12MoveState instance = null;
    public static MB12MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB12MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB12Transition[] transitions = { MB12MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB12Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB12Base> controller) {
        controller.ObjectBase.MB12Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB12Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB12Move.MoveDirect();
    }

    protected override Transition<MB12Base>[] GetTransitions() {
        return transitions;
    }
}
