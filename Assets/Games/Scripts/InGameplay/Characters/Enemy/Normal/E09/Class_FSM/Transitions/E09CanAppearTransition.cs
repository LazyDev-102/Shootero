

using Class_FSM;

public class E09CanAppearTransition : E09Transition {
    #region Singleton
    public E09CanAppearTransition() {

    }
    private static E09CanAppearTransition instance = null;
    public static E09CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E09CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E09Base> controller) {
        bool isTransition = controller.ObjectBase.E09Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E09AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E09Base> controller) {
    }
}
