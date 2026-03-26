

using Class_FSM;

public class E11CanAppearTransition : E11Transition {
    #region Singleton
    public E11CanAppearTransition() {

    }
    private static E11CanAppearTransition instance = null;
    public static E11CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E11CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E11Base> controller) {
        bool isTransition = controller.ObjectBase.E11Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E11MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E11Base> controller) {
    }
}
