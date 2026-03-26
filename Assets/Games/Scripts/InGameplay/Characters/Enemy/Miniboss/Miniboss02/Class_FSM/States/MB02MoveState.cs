using Class_FSM;
using UnityEngine;

public class MB02MoveState : MB02State {

    #region Singleton
    public MB02MoveState() {

    }
    private static MB02MoveState instance = null;
    public static MB02MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB02MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB02Transition[] transitions = { MB02MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB02Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB02Base> controller) {
        controller.ObjectBase.MB02Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB02Move.MoveDirect();
    }

    protected override Transition<MB02Base>[] GetTransitions() {
        return transitions;
    }
}
