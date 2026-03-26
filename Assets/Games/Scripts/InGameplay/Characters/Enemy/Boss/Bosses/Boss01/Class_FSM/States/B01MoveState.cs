

using Class_FSM;
using UnityEngine;

public class B01MoveState : B01State {
    #region Singleton
    public B01MoveState() {

    }
    private static B01MoveState instance = null;
    public static B01MoveState Instance {
        get {
            if (instance == null) {
                instance = new B01MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B01Transition[] transitions = { B01MoveCompleteTransition.Instance, B01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B01Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B01Base> controller) {
        controller.ObjectBase.B01Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B01Move.MoveDirect();
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
