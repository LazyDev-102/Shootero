using Class_FSM;
using UnityEngine;

public class XMB01AttackState : XMB01State {
    #region Singleton
    public XMB01AttackState() {

    }
    private static XMB01AttackState instance = null;
    public static XMB01AttackState Instance {
        get {
            if (instance == null) {
                instance = new XMB01AttackState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01Transition[] transitions = { XMB01EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<XMB01Base> controller) {
    }

    protected override void DoStartActions(StateController<XMB01Base> controller) {
        XMB01Attack attack = controller.ObjectBase.XMB01Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<XMB01Base> controller) {
    }

    protected override Transition<XMB01Base>[] GetTransitions() {
        return transitions;
    }
}
