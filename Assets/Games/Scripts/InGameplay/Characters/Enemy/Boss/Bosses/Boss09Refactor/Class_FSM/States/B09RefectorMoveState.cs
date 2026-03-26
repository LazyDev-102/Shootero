

using Class_FSM;
using UnityEngine;

public class B09RefectorMoveState : B09RefectorState {
    #region Singleton
    public B09RefectorMoveState() {

    }
    private static B09RefectorMoveState instance = null;
    public static B09RefectorMoveState Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorMoveState();
            }
            return instance;
        }
    }
    #endregion
    private B09RefectorTransition[] transitions = { B09RefectorMoveCompleteTransition.Instance, B09RefectorCanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.B09RefectorMove.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.B09RefectorMove.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }
}
