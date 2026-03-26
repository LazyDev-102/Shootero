

using Class_FSM;

public class B02EndMoveRageTransition : B02Transition {
    #region Singleton
    public B02EndMoveRageTransition() {

    }
    private static B02EndMoveRageTransition instance = null;
    public static B02EndMoveRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B02EndMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.B02Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B02AttackRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B02Base> controller) {
    }
}
