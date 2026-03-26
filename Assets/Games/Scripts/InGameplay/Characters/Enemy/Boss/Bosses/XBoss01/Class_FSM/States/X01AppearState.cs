

using Class_FSM;
using UnityEngine;

public class XB01AppearState : XB01State {
    #region Singleton
    public XB01AppearState() {

    }
    private static XB01AppearState instance = null;
    public static XB01AppearState Instance {
        get {
            if (instance == null) {
                instance = new XB01AppearState();
            }
            return instance;
        }
    }
    #endregion

    private XB01Transition[] transitions = { XB01AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<XB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
        controller.ObjectBase.XB01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
