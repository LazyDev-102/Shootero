using Class_FSM;
using UnityEngine;

public class MB02AttackState : MB02State {
    #region Singleton
    public MB02AttackState() {

    }
    private static MB02AttackState instance = null;
    public static MB02AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB02AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB02Transition[] transitions = { MB02EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB02Base> controller) {
    }

    protected override void DoStartActions(StateController<MB02Base> controller) {
        MB02Attack attack = controller.ObjectBase.MB02Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB02Base> controller) {
    }

    protected override Transition<MB02Base>[] GetTransitions() {
        return transitions;
    }
}
