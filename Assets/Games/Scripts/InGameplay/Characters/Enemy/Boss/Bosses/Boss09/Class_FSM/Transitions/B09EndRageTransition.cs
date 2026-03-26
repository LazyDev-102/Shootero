

using Class_FSM;

public class B09EndRageTransition : B09Transition {

    #region Singleton
    public B09EndRageTransition() {

    }
    private static B09EndRageTransition instance = null;
    public static B09EndRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B09EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = !controller.ObjectBase.B09Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B09MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
