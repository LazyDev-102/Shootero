using Class_FSM;
using UnityEngine;

public class MB04MoveState : MB04State {

    #region Singleton
    public MB04MoveState() {

    }
    private static MB04MoveState instance = null;
    public static MB04MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB04MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB04Transition[] transitions = { MB04MoveCompleteTransition.Instance };
    protected override void DoEndActions(StateController<MB04Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB04Base> controller) {
        controller.ObjectBase.MB04Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<MB04Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB04Move.MoveDirect();
    }

    protected override Transition<MB04Base>[] GetTransitions() {
        return transitions;
    }
}
