

using Class_FSM;
using UnityEngine;

public class B09MoveState : B09State {
    #region Singleton
    public B09MoveState() {

    }
    private static B09MoveState instance = null;
    public static B09MoveState Instance {
        get {
            if (instance == null) {
                instance = new B09MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B09Transition[] transitions = { B09MoveCompleteTransition.Instance, B09CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B09Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}
