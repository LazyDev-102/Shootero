using Class_FSM;
using UnityEngine;

public class XMB01AppearState : XMB01State {

    #region Singleton
    public XMB01AppearState() {

    }
    private static XMB01AppearState instance = null;
    public static XMB01AppearState Instance {
        get {
            if (instance == null) {
                instance = new XMB01AppearState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01Transition[] transitions = { XMB01AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.XMB01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<XMB01Base> controller) {
    }

    protected override Transition<XMB01Base>[] GetTransitions() {
        return transitions;
    }
}
