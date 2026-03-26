using Class_FSM;
using UnityEngine;

public class MB05MoveState : MB05State {

    #region Singleton
    public MB05MoveState() {

    }
    private static MB05MoveState instance = null;
    public static MB05MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB05MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB05Transition[] transitions = { MB05MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB05Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB05Base> controller) {
        controller.ObjectBase.MB05Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB05Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB05Move.MoveDirect();
    }

    protected override Transition<MB05Base>[] GetTransitions() {
        return transitions;
    }
}
