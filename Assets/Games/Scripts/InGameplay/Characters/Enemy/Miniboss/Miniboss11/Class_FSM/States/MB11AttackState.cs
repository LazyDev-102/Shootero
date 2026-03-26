using Class_FSM;
using UnityEngine;

public class MB11AttackState : MB11State {
    #region Singleton
    public MB11AttackState() {

    }
    private static MB11AttackState instance = null;
    public static MB11AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB11AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB11Transition[] transitions = { MB11EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB11Base> controller) {
    }

    protected override void DoStartActions(StateController<MB11Base> controller) {
        MB11Attack attack = controller.ObjectBase.MB11Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB11Base> controller) {
    }

    protected override Transition<MB11Base>[] GetTransitions() {
        return transitions;
    }
}
