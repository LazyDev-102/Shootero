using Class_FSM;
using UnityEngine;

public class MB06AttackState : MB06State {
    #region Singleton
    public MB06AttackState() {

    }
    private static MB06AttackState instance = null;
    public static MB06AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB06AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB06Transition[] transitions = { MB06EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB06Base> controller) {
    }

    protected override void DoStartActions(StateController<MB06Base> controller) {
        MB06Attack attack = controller.ObjectBase.MB06Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB06Base> controller) {
    }

    protected override Transition<MB06Base>[] GetTransitions() {
        return transitions;
    }
}
