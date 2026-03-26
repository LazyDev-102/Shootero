using Class_FSM;
using UnityEngine;

public class B14ChildAttackState : B14ChildState {
    #region Singleton
    public B14ChildAttackState() {

    }
    private static B14ChildAttackState instance = null;
    public static B14ChildAttackState Instance {
        get {
            if (instance == null) {
                instance = new B14ChildAttackState();
            }
            return instance;
        }
    }
    #endregion

    private B14ChildTransition[] transitions = { B14ChildEndAttackTransition.Instance };

    protected override void DoEndActions(StateController<B14ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<B14ChildBase> controller) {
        B14ChildAttack attack = controller.ObjectBase.B14ChildAttack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B14ChildBase> controller) {
    }

    protected override Transition<B14ChildBase>[] GetTransitions() {
        return transitions;
    }
}
