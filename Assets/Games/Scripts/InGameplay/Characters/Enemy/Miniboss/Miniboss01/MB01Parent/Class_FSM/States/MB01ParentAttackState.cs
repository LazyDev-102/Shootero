using Class_FSM;
using UnityEngine;

public class MB01ParentAttackState : MB01ParentState {
    #region Singleton
    public MB01ParentAttackState() {

    }
    private static MB01ParentAttackState instance = null;
    public static MB01ParentAttackState Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentAttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB01ParentTransition[] transitions = { MB01ParentEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB01ParentBase> controller) {
        MB01ParentAttack attack = controller.ObjectBase.MB01ParentAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB01ParentBase> controller) {
    }

    protected override Transition<MB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
