

using Class_FSM;
using UnityEngine;

public class B11IdleState : B11State {
    #region Singleton
    public B11IdleState() {

    }
    private static B11IdleState instance = null;
    public static B11IdleState Instance {
        get {
            if (instance == null) {
                instance = new B11IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B11Transition[] transitions = { B11CanRageTransition.Instance, B11CanAttackTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<B11Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
        controller.ObjectBase.CountdownIdle();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
