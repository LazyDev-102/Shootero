using Class_FSM;
using UnityEngine;

public class MB01AttackState : MB01State {
    #region Singleton
    public MB01AttackState() {

    }
    private static MB01AttackState instance = null;
    public static MB01AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB01AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB01Transition[] transitions = { MB01EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB01Base> controller) {
    }

    protected override void DoStartActions(StateController<MB01Base> controller) {
        MB01Attack attack = controller.ObjectBase.MB01Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB01Base> controller) {
    }

    protected override Transition<MB01Base>[] GetTransitions() {
        return transitions;
    }
}
