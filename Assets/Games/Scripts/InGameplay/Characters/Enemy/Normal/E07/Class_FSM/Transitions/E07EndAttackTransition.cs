

using Class_FSM;

public class E07EndAttackTransition : E07Transition {
    #region Singleton
    public E07EndAttackTransition() {

    }
    private static E07EndAttackTransition instance = null;
    public static E07EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new E07EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E07Base> controller) {
        //bool isTransition = controller.ObjectBase.E07Attack.IsEndAttackCircle() && controller.ObjectBase.E07Move.CompleteMoveToTarget();
        //bool isTransition = controller.ObjectBase.E07Move.CompleteMoveToTarget();
        //bool isTransition = controller.ObjectBase.E07Move.HasOutBorder();
        bool isTransition = !controller.ObjectBase.E07Attack.IsAttacking();
        if (isTransition) {
            //controller.TransitionToState(E07AppearState.Instance, this);
            controller.TransitionToState(E07StartState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E07Base> controller) {
    }
}
