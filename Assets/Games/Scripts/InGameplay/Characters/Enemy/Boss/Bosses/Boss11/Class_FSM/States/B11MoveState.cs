

using Class_FSM;
using UnityEngine;

public class B11MoveState : B11State {
    #region Singleton
    public B11MoveState() {

    }
    private static B11MoveState instance = null;
    public static B11MoveState Instance {
        get {
            if (instance == null) {
                instance = new B11MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B11Transition[] transitions = { B11MoveCompleteTransition.Instance, B11CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B11Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
