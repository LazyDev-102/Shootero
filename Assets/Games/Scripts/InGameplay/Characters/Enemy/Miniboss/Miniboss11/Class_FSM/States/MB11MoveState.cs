using Class_FSM;
using UnityEngine;

public class MB11MoveState : MB11State {

    #region Singleton
    public MB11MoveState() {

    }
    private static MB11MoveState instance = null;
    public static MB11MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB11MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB11Transition[] transitions = { MB11MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB11Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB11Base> controller) {
        controller.ObjectBase.MB11Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB11Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB11Move.MoveDirect();
    }

    protected override Transition<MB11Base>[] GetTransitions() {
        return transitions;
    }
}
