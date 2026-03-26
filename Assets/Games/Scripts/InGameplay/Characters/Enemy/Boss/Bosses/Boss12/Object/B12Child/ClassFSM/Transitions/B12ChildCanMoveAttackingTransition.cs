

using Class_FSM;

public class B12ChildCanMoveAttackingTransition : B12ChildTransition {
    #region Singleton
    public B12ChildCanMoveAttackingTransition() {

    }
    private static B12ChildCanMoveAttackingTransition instance = null;
    public static B12ChildCanMoveAttackingTransition Instance {
        get {
            if(instance == null) {
                instance = new B12ChildCanMoveAttackingTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B12ChildAttack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B12ChildMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12ChildBase> controller) {
    }
}
