

using Class_FSM;
using UnityEngine;

public class B14IdleState : B14State {
    #region Singleton
    public B14IdleState() {

    }
    private static B14IdleState instance = null;
    public static B14IdleState Instance {
        get {
            if (instance == null) {
                instance = new B14IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B14Transition[] transitions = { B14CanRageTransition.Instance, B14CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B14Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
