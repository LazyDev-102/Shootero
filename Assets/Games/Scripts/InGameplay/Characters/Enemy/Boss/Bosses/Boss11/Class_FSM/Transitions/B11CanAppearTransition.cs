

using Class_FSM;

public class B11CanAppearTransition : B11Transition {
    #region Singleton
    public B11CanAppearTransition() {

    }
    private static B11CanAppearTransition instance = null;
    public static B11CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B11CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = controller.ObjectBase.B11Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B11AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }
}
