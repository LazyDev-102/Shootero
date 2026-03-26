using Class_FSM;
using UnityEngine;

public class MB09ParentAttackState : MB09ParentState {
    #region Singleton
    public MB09ParentAttackState() {

    }
    private static MB09ParentAttackState instance = null;
    public static MB09ParentAttackState Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentAttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB09ParentTransition[] transitions = { MB09ParentEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB09ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB09ParentBase> controller) {
        MB09ParentAttack attack = controller.ObjectBase.MB09ParentAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB09ParentBase> controller) {
    }

    protected override Transition<MB09ParentBase>[] GetTransitions() {
        return transitions;
    }
}
