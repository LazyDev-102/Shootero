

using Class_FSM;
using UnityEngine;

public class XB01MoveState : XB01State {
    #region Singleton
    public XB01MoveState() {

    }
    private static XB01MoveState instance = null;
    public static XB01MoveState Instance {
        get {
            if (instance == null) {
                instance = new XB01MoveState();
            }
            return instance;
        }
    }
    #endregion
    private XB01Transition[] transitions = { XB01MoveCompleteTransition.Instance, XB01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<XB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
        controller.ObjectBase.XB01Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.XB01Move.MoveDirect();
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
