

using Class_FSM;

public class B01EndRageTransition : B01Transition {

    #region Singleton
    public B01EndRageTransition() {

    }
    private static B01EndRageTransition instance = null;
    public static B01EndRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B01EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = controller.ObjectBase.B01Move.HasOutBorder();
        if(isTransition) {
            controller.TransitionToState(B01StartState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B01Base> controller) {
    }
}
