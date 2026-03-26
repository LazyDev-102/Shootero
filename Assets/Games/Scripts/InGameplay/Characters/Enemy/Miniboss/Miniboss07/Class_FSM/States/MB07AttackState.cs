using Class_FSM;
using UnityEngine;

public class MB07AttackState : MB07State {
    #region Singleton
    public MB07AttackState() {

    }
    private static MB07AttackState instance = null;
    public static MB07AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB07AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB07Transition[] transitions = { MB07EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB07Base> controller) {
    }

    protected override void DoStartActions(StateController<MB07Base> controller) {
        MB07Attack attack = controller.ObjectBase.MB07Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB07Base> controller) {
    }

    protected override Transition<MB07Base>[] GetTransitions() {
        return transitions;
    }
}
