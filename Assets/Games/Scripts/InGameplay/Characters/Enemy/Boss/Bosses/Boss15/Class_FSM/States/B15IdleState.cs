

using Class_FSM;
using UnityEngine;

public class B15IdleState : B15State {
    #region Singleton
    public B15IdleState() {

    }
    private static B15IdleState instance = null;
    public static B15IdleState Instance {
        get {
            if (instance == null) {
                instance = new B15IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B15Transition[] transitions = { B15CanAttackTransition.Instance, B15CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B15Base> controller) {
        // controller.ObjectBase.B15Move.EndMoveIdle();
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B15Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
        //controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
