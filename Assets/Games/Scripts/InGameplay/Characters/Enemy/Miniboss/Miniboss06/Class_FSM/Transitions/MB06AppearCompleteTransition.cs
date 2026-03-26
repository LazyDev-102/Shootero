
using Class_FSM;

public class MB06AppearCompleteTransition : MB06Transition {

    #region Singleton
    public MB06AppearCompleteTransition() {

    }
    private static MB06AppearCompleteTransition instance = null;
    public static MB06AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB06AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB06Base> controller) {
        bool isTransition = controller.ObjectBase.MB06Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB06IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB06Base> controller) {
    }
}
