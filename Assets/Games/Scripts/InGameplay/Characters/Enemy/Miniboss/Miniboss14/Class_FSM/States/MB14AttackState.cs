using Class_FSM;
using UnityEngine;

public class MB14AttackState : MB14State {
    #region Singleton
    public MB14AttackState() {

    }
    private static MB14AttackState instance = null;
    public static MB14AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB14AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB14Transition[] transitions = { MB14EndAttackTransition.Instance, MB14CanSpecialTransition.Instance };

    protected override void DoEndActions(StateController<MB14Base> controller) {
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
        MB14Attack attack = controller.ObjectBase.MB14Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return transitions;
    }
}
