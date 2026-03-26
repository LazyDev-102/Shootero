

using Class_FSM;

public class E09EndAttackTransition : E09Transition {
    #region Singleton
    public E09EndAttackTransition() {

    }
    private static E09EndAttackTransition instance = null;
    public static E09EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E09EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E09Base> controller) {
        bool isTransition = controller.ObjectBase.E09Attack.IsEndLaser();
        if(isTransition) {
            controller.TransitionToState(E09AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E09Base> controller) {
    }
}
