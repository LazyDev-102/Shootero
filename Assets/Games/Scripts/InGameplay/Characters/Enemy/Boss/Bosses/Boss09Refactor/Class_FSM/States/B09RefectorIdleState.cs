

using Class_FSM;
using UnityEngine;

public class B09RefectorIdleState : B09RefectorState {
    #region Singleton
    public B09RefectorIdleState() {

    }
    private static B09RefectorIdleState instance = null;
    public static B09RefectorIdleState Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorIdleState();
            }
            return instance;
        }
    }
    #endregion
    private B09RefectorTransition[] transitions = { B09RefectorCanRageTransition.Instance, B09RefectorCanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }
}
