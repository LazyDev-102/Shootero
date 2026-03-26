using Class_FSM;
using UnityEngine;

public class XMB02AppearState : XMB02State {

    #region Singleton
    public XMB02AppearState() {

    }
    private static XMB02AppearState instance = null;
    public static XMB02AppearState Instance {
        get {
            if (instance == null) {
                instance = new XMB02AppearState();
            }
            return instance;
        }
    }
    #endregion

    private XMB02Transition[] transitions = { XMB02AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.XMB02Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<XMB02Base> controller) {
    }

    protected override Transition<XMB02Base>[] GetTransitions() {
        return transitions;
    }
}
