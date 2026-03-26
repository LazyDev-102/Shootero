

using Class_FSM;

public class B12ChildCanMoveAppearTransition : B12ChildTransition {
    #region Singleton
    public B12ChildCanMoveAppearTransition() {

    }
    private static B12ChildCanMoveAppearTransition instance = null;
    public static B12ChildCanMoveAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B12ChildCanMoveAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B12ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B12ChildMove.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B12ChildMoveAppearState.Instance, this);
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
