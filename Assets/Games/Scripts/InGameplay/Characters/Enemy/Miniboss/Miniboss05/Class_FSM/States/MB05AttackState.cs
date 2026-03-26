using Class_FSM;
using UnityEngine;

public class MB05AttackState : MB05State {
    #region Singleton
    public MB05AttackState() {

    }
    private static MB05AttackState instance = null;
    public static MB05AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB05AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB05Transition[] transitions = { MB05EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB05Base> controller) {
    }

    protected override void DoStartActions(StateController<MB05Base> controller) {
        MB05Attack attack = controller.ObjectBase.MB05Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB05Base> controller) {
    }

    protected override Transition<MB05Base>[] GetTransitions() {
        return transitions;
    }
}
