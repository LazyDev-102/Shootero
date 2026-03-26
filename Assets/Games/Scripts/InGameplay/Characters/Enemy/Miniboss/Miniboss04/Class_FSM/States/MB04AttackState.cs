using Class_FSM;
using UnityEngine;

public class MB04AttackState : MB04State {

    #region Singleton
    public MB04AttackState() {

    }
    private static MB04AttackState instance = null;
    public static MB04AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB04AttackState();
            }
            return instance;
        }
    }
    #endregion


    private MB04Transition[] transitions = { MB04EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB04Base> controller) {
    }

    protected override void DoStartActions(StateController<MB04Base> controller) {
        MB04Attack attack = controller.ObjectBase.MB04Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB04Base> controller) {
    }

    protected override Transition<MB04Base>[] GetTransitions() {
        return transitions;
    }
}
