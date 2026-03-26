

using Class_FSM;
using UnityEngine;

public class B06MoveState : B06State {
    #region Singleton
    public B06MoveState() {

    }
    private static B06MoveState instance = null;
    public static B06MoveState Instance {
        get {
            if (instance == null) {
                instance = new B06MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B06Transition[] transitions = { B06MoveCompleteTransition.Instance, B06CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B06Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
