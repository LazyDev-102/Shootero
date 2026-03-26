

using Class_FSM;

public class B03EndMoveRageTransition : B03Transition {
    #region Singleton
    public B03EndMoveRageTransition() {

    }
    private static B03EndMoveRageTransition instance = null;
    public static B03EndMoveRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B03EndMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.B03Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B03AttackRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
