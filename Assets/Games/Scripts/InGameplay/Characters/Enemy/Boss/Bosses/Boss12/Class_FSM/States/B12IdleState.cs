

using Class_FSM;
using UnityEngine;

public class B12IdleState : B12State {
    #region Singleton
    public B12IdleState() {

    }
    private static B12IdleState instance = null;
    public static B12IdleState Instance {
        get {
            if (instance == null) {
                instance = new B12IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B12Transition[] transitions = { B12CanRageTransition.Instance, B12CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B12Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
