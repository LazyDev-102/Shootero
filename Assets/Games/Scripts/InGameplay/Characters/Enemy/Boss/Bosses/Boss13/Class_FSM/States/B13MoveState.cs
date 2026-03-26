

using Class_FSM;
using UnityEngine;

public class B13MoveState : B13State {
    #region Singleton
    public B13MoveState() {

    }
    private static B13MoveState instance = null;
    public static B13MoveState Instance {
        get {
            if (instance == null) {
                instance = new B13MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B13Transition[] transitions = { B13MoveCompleteTransition.Instance, B13CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B13Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
