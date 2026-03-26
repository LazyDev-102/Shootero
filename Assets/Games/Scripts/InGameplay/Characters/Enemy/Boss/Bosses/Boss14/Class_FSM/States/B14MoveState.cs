

using Class_FSM;
using UnityEngine;

public class B14MoveState : B14State {
    #region Singleton
    public B14MoveState() {

    }
    private static B14MoveState instance = null;
    public static B14MoveState Instance {
        get {
            if (instance == null) {
                instance = new B14MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B14Transition[] transitions = { B14MoveCompleteTransition.Instance, B14CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B14Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
