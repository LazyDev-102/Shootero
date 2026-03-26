

using Class_FSM;
using UnityEngine;

public class B05IdleState : B05State {
    #region Singleton
    public B05IdleState() {

    }
    private static B05IdleState instance = null;
    public static B05IdleState Instance {
        get {
            if (instance == null) {
                instance = new B05IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B05Transition[] transitions = { B05CanRageTransition.Instance, B05CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B05Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
