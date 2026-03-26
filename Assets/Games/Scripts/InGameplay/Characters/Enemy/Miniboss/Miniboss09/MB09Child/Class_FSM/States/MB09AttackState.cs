using Class_FSM;
using UnityEngine;

public class MB09AttackState : MB09State {
    #region Singleton
    public MB09AttackState() {

    }
    private static MB09AttackState instance = null;
    public static MB09AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB09AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB09Transition[] transitions = { MB09EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB09Base> controller) {
    }

    protected override void DoStartActions(StateController<MB09Base> controller) {
        MB09Attack attack = controller.ObjectBase.MB09Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB09Base> controller) {
    }

    protected override Transition<MB09Base>[] GetTransitions() {
        return transitions;
    }
}
