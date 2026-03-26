using Class_FSM;
using UnityEngine;

public class MB15ChildAttackState : MB15ChildState {
    #region Singleton
    public MB15ChildAttackState() {

    }
    private static MB15ChildAttackState instance = null;
    public static MB15ChildAttackState Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildAttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ChildTransition[] transitions = { MB15ChildEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB15ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<MB15ChildBase> controller) {
        MB15ChildAttack attack = controller.ObjectBase.MB15ChildAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB15ChildBase> controller) {
    }

    protected override Transition<MB15ChildBase>[] GetTransitions() {
        return transitions;
    }
}
