using Class_FSM;
using UnityEngine;

public class MB12AttackState : MB12State {
    #region Singleton
    public MB12AttackState() {

    }
    private static MB12AttackState instance = null;
    public static MB12AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB12AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB12Transition[] transitions = { MB12EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB12Base> controller) {
    }

    protected override void DoStartActions(StateController<MB12Base> controller) {
        MB12Attack attack = controller.ObjectBase.MB12Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB12Base> controller) {
    }

    protected override Transition<MB12Base>[] GetTransitions() {
        return transitions;
    }
}
