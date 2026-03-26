

using Class_FSM;
using UnityEngine;

public class B13IdleState : B13State {
    #region Singleton
    public B13IdleState() {

    }
    private static B13IdleState instance = null;
    public static B13IdleState Instance {
        get {
            if (instance == null) {
                instance = new B13IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B13Transition[] transitions = { B13CanRageTransition.Instance, B13CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B13Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
