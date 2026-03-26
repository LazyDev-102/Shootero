using Class_FSM;
using UnityEngine;

public class MB06MoveState : MB06State {

    #region Singleton
    public MB06MoveState() {

    }
    private static MB06MoveState instance = null;
    public static MB06MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB06MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB06Transition[] transitions = { MB06MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB06Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB06Base> controller) {
        controller.ObjectBase.MB06Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB06Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB06Move.MoveDirect();
    }

    protected override Transition<MB06Base>[] GetTransitions() {
        return transitions;
    }
}
