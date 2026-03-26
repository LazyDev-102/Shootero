using Class_FSM;
using UnityEngine;

public class HMB01ParentAttackState : HMB01ParentState {
    #region Singleton
    public HMB01ParentAttackState() {

    }
    private static HMB01ParentAttackState instance = null;
    public static HMB01ParentAttackState Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentAttackState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01ParentTransition[] transitions = { HMB01ParentEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<HMB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<HMB01ParentBase> controller) {
        HMB01ParentAttack attack = controller.ObjectBase.HMB01ParentAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<HMB01ParentBase> controller) {
    }

    protected override Transition<HMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
