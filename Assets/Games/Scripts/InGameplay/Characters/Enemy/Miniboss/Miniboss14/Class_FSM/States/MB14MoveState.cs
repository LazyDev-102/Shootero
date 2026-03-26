using Class_FSM;
using UnityEngine;

public class MB14MoveState : MB14State {

    #region Singleton
    public MB14MoveState() {

    }
    private static MB14MoveState instance = null;
    public static MB14MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB14MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB14Transition[] transitions = { MB14MoveCompleteTransition.Instance, MB14CanSpecialTransition.Instance };

    protected override void DoEndActions(StateController<MB14Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
        controller.ObjectBase.MB14Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB14Move.MoveDirect();
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return transitions;
    }
}
