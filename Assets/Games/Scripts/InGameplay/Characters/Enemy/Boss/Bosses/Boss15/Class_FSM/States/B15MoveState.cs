

using Class_FSM;
using UnityEngine;

public class B15MoveState : B15State {
    #region Singleton
    public B15MoveState() {

    }
    private static B15MoveState instance = null;
    public static B15MoveState Instance {
        get {
            if (instance == null) {
                instance = new B15MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B15Transition[] transitions = { B15CanAttackTransition.Instance, B15CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B15Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B15Base> controller) {
        controller.ObjectBase.B15Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
        //controller.ObjectBase.LookTarget();
        //controller.ObjectBase.B15Move.MoveDirect();
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
