

using Class_FSM;

public class E04HasDieTransition : E04Transition{
    #region Singleton
    public E04HasDieTransition() {

    }
    private static E04HasDieTransition instance = null;
    public static E04HasDieTransition Instance {
        get {
            if(instance == null) {
                instance = new E04HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E04Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E04DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E04Base> controller) {
    }
}
