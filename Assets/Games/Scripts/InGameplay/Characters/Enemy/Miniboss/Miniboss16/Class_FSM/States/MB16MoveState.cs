using Class_FSM;
using UnityEngine;

public class MB16MoveState : MB16State {

    #region Singleton
    public MB16MoveState() {

    }
    private static MB16MoveState instance = null;
    public static MB16MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB16MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB16Transition[] transitions = { MB16MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB16Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB16Base> controller) {
        controller.ObjectBase.MB16Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB16Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB16Move.MoveDirect();
    }

    protected override Transition<MB16Base>[] GetTransitions() {
        return transitions;
    }
}
