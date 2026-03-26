

using Class_FSM;

public class E08EndAttackTransition : E08Transition {
    #region Singleton
    public E08EndAttackTransition() {

    }
    private static E08EndAttackTransition instance = null;
    public static E08EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E08EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E08Base> controller) {
        bool isTransition = controller.ObjectBase.E08Attack.IsEndLaser();
        if(isTransition) {
            controller.TransitionToState(E08AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E08Base> controller) {
    }
}
