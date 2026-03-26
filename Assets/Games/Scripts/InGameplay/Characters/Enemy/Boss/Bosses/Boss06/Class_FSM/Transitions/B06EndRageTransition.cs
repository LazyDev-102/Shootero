

using Class_FSM;

public class B06EndRageTransition : B06Transition {

    #region Singleton
    public B06EndRageTransition() {

    }
    private static B06EndRageTransition instance = null;
    public static B06EndRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B06EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = !controller.ObjectBase.B06Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B06IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
