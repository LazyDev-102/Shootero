using Class_FSM;
using UnityEngine;

public class XMB02AttackState : XMB02State {
    #region Singleton
    public XMB02AttackState() {

    }
    private static XMB02AttackState instance = null;
    public static XMB02AttackState Instance {
        get {
            if (instance == null) {
                instance = new XMB02AttackState();
            }
            return instance;
        }
    }
    #endregion

    private XMB02Transition[] transitions = { XMB02EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<XMB02Base> controller) {
    }

    protected override void DoStartActions(StateController<XMB02Base> controller) {
        XMB02Attack attack = controller.ObjectBase.XMB02Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<XMB02Base> controller) {
    }

    protected override Transition<XMB02Base>[] GetTransitions() {
        return transitions;
    }
}
