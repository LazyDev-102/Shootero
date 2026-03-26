

using Class_FSM;
using UnityEngine;

public class B06IdleState : B06State {
    #region Singleton
    public B06IdleState() {

    }
    private static B06IdleState instance = null;
    public static B06IdleState Instance {
        get {
            if (instance == null) {
                instance = new B06IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B06Transition[] transitions = { B06CanRageTransition.Instance, B06CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B06Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
