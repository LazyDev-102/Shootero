using Class_FSM;
using UnityEngine;

public class HMB02AttackState : HMB02State {
    #region Singleton
    public HMB02AttackState() {

    }
    private static HMB02AttackState instance = null;
    public static HMB02AttackState Instance {
        get {
            if (instance == null) {
                instance = new HMB02AttackState();
            }
            return instance;
        }
    }
    #endregion

    private HMB02Transition[] transitions = { HMB02EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<HMB02Base> controller) {
    }

    protected override void DoStartActions(StateController<HMB02Base> controller) {
        HMB02Attack attack = controller.ObjectBase.HMB02Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<HMB02Base> controller) {
    }

    protected override Transition<HMB02Base>[] GetTransitions() {
        return transitions;
    }
}
