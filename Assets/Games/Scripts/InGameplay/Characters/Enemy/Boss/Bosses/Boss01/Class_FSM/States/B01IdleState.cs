

using Class_FSM;
using UnityEngine;

public class B01IdleState : B01State {
    #region Singleton
    public B01IdleState() {

    }
    private static B01IdleState instance = null;
    public static B01IdleState Instance {
        get {
            if (instance == null) {
                instance = new B01IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B01Transition[] transitions = { B01CanAttackTransition.Instance, B01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B01Base> controller) {
        // controller.ObjectBase.B01Move.EndMoveIdle();
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B01Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
