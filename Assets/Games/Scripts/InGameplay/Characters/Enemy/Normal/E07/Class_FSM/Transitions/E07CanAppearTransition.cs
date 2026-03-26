

using Class_FSM;

public class E07CanAppearTransition : E07Transition {
    #region Singleton
    public E07CanAppearTransition() {

    }
    private static E07CanAppearTransition instance = null;
    public static E07CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E07CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E07Base> controller) {
        bool isTransition = controller.ObjectBase.E07Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E07AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E07Base> controller) {
    }
}
