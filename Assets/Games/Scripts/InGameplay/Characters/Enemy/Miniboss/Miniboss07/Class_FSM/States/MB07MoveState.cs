using Class_FSM;
using UnityEngine;

public class MB07MoveState : MB07State {

    #region Singleton
    public MB07MoveState() {

    }
    private static MB07MoveState instance = null;
    public static MB07MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB07MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB07Transition[] transitions = { MB07MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB07Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB07Base> controller) {
        controller.ObjectBase.MB07Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB07Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB07Move.MoveDirect();
    }

    protected override Transition<MB07Base>[] GetTransitions() {
        return transitions;
    }
}
