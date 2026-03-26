using Class_FSM;
using UnityEngine;

public class XMB01ParentAttackState : XMB01ParentState {
    #region Singleton
    public XMB01ParentAttackState() {

    }
    private static XMB01ParentAttackState instance = null;
    public static XMB01ParentAttackState Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentAttackState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01ParentTransition[] transitions = { XMB01ParentEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<XMB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<XMB01ParentBase> controller) {
        XMB01ParentAttack attack = controller.ObjectBase.XMB01ParentAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<XMB01ParentBase> controller) {
    }

    protected override Transition<XMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
