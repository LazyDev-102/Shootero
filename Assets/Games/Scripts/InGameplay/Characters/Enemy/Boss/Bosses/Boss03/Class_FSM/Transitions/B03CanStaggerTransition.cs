

using Class_FSM;

public class B03CanStaggerTransition : B03Transition {
    #region Singleton
    public B03CanStaggerTransition() {

    }
    private static B03CanStaggerTransition instance = null;
    public static B03CanStaggerTransition Instance {
        get {
            if(instance == null) {
                instance = new B03CanStaggerTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.CanStagger();
        if(isTransition) {
            controller.TransitionToState(B03StaggerState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Attack.StopAttack();
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
