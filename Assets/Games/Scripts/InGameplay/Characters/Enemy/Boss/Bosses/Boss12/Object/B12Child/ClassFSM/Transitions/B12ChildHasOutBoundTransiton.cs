

using Class_FSM;

public class B12ChildHasOutBoundTransiton : B12ChildTransition {
    #region Singleton
    public B12ChildHasOutBoundTransiton() {

    }
    private static B12ChildHasOutBoundTransiton instance = null;
    public static B12ChildHasOutBoundTransiton Instance {
        get {
            if(instance == null) {
                instance = new B12ChildHasOutBoundTransiton();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B12ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B12ChildMove.HasOutBorder();
        if(isTransition) {
            controller.TransitionToState(B12ChildIdleState.Instance, this);
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
