

using Class_FSM;

public class E02HasOutBoundTransiton : E02Transition {
    #region Singleton
    public E02HasOutBoundTransiton() {

    }
    private static E02HasOutBoundTransiton instance = null;
    public static E02HasOutBoundTransiton Instance {
        get {
            if(instance == null) {
                instance = new E02HasOutBoundTransiton();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<E02Base> controller) {
        bool isTransition = controller.ObjectBase.E02Move.HasOutBorder();
        if(isTransition) {
            controller.TransitionToState(E02IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E02Base> controller) {
    }
}
