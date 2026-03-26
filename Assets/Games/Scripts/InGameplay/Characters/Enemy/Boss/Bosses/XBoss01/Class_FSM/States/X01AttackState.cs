

using Class_FSM;
using UnityEngine;

public class XB01AttackState : XB01State {

    #region Singleton
    public XB01AttackState() {

    }
    private static XB01AttackState instance = null;
    public static XB01AttackState Instance {
        get {
            if (instance == null) {
                instance = new XB01AttackState();
            }
            return instance;
        }
    }
    #endregion
    private XB01Transition[] transitions = { XB01EndAttackTransition.Instance, XB01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<XB01Base> controller) {

    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
        controller.ObjectBase.XB01Attack.ChooseAttack();
        controller.ObjectBase.XB01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {

    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
