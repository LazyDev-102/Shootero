

using Class_FSM;
using UnityEngine;

public class B12MoveState : B12State {
    #region Singleton
    public B12MoveState() {

    }
    private static B12MoveState instance = null;
    public static B12MoveState Instance {
        get {
            if (instance == null) {
                instance = new B12MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B12Transition[] transitions = { B12MoveCompleteTransition.Instance, B12CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B12Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.MoveDirect();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
