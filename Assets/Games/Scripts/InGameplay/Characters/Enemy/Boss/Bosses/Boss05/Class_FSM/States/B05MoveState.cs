

using Class_FSM;
using UnityEngine;

public class B05MoveState : B05State {
    #region Singleton
    public B05MoveState() {

    }
    private static B05MoveState instance = null;
    public static B05MoveState Instance {
        get {
            if (instance == null) {
                instance = new B05MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B05Transition[] transitions = { B05MoveCompleteTransition.Instance, B05CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B05Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
