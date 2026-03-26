
using Class_FSM;

public class MB11AppearCompleteTransition : MB11Transition {

    #region Singleton
    public MB11AppearCompleteTransition() {

    }
    private static MB11AppearCompleteTransition instance = null;
    public static MB11AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB11AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB11Base> controller) {
        bool isTransition = controller.ObjectBase.MB11Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB11IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB11Base> controller) {
    }
}
