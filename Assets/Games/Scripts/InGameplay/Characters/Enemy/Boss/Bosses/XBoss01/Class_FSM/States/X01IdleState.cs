

using Class_FSM;
using UnityEngine;

public class XB01IdleState : XB01State {
    #region Singleton
    public XB01IdleState() {

    }
    private static XB01IdleState instance = null;
    public static XB01IdleState Instance {
        get {
            if (instance == null) {
                instance = new XB01IdleState();
            }
            return instance;
        }
    }
    #endregion
    private XB01Transition[] transitions = { XB01CanAttackTransition.Instance, XB01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<XB01Base> controller) {
        // controller.ObjectBase.XB01Move.EndMoveIdle();
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
