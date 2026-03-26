

using Class_FSM;

public class E06HasDieTransition : E06Transition {
    #region Singleton
    public E06HasDieTransition() {

    }
    private static E06HasDieTransition instance = null;
    public static E06HasDieTransition Instance {
        get {
            if(instance == null) {
                instance = new E06HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E06Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E06DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E06Base> controller) {
    }
}
