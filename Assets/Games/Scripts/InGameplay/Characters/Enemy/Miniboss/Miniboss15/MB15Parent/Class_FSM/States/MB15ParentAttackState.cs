using Class_FSM;
using UnityEngine;

public class MB15ParentAttackState : MB15ParentState {
    #region Singleton
    public MB15ParentAttackState() {

    }
    private static MB15ParentAttackState instance = null;
    public static MB15ParentAttackState Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentAttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ParentTransition[] transitions = { MB15ParentEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB15ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB15ParentBase> controller) {
        MB15ParentAttack attack = controller.ObjectBase.MB15ParentAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB15ParentBase> controller) {
    }

    protected override Transition<MB15ParentBase>[] GetTransitions() {
        return transitions;
    }
}
