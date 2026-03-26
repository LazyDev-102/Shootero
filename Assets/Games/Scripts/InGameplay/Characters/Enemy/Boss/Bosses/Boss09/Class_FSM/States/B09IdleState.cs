

using Class_FSM;
using UnityEngine;

public class B09IdleState : B09State {
    #region Singleton
    public B09IdleState() {

    }
    private static B09IdleState instance = null;
    public static B09IdleState Instance {
        get {
            if (instance == null) {
                instance = new B09IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B09Transition[] transitions = { B09CanRageTransition.Instance, B09CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B09Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}
