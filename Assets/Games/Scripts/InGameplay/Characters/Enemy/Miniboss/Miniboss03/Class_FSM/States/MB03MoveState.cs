using Class_FSM;
using UnityEngine;

public class MB03MoveState : MB03State {

    #region Singleton
    public MB03MoveState() {

    }
    private static MB03MoveState instance = null;
    public static MB03MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB03MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB03Transition[] transitions = { MB03MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB03Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB03Base> controller) {
        controller.ObjectBase.MB03Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB03Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB03Move.MoveDirect();
    }

    protected override Transition<MB03Base>[] GetTransitions() {
        return transitions;
    }
}
