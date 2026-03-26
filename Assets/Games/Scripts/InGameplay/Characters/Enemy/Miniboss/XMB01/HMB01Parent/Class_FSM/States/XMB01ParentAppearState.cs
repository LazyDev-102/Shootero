using Class_FSM;
using UnityEngine;

public class XMB01ParentAppearState : XMB01ParentState {

    #region Singleton
    public XMB01ParentAppearState() {

    }
    private static XMB01ParentAppearState instance = null;
    public static XMB01ParentAppearState Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentAppearState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01ParentTransition[] transitions = { XMB01ParentAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.MinibossMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<XMB01ParentBase> controller) {
    }

    protected override Transition<XMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
