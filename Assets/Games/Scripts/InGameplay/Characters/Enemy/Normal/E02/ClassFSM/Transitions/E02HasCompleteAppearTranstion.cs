

using Class_FSM;

public class E02HasCompleteAppearTranstion : E02Transition{
    #region Singleton
    public E02HasCompleteAppearTranstion() {

    }
    private static E02HasCompleteAppearTranstion instance = null;
    public static E02HasCompleteAppearTranstion Instance {
        get {
            if(instance == null) {
                instance = new E02HasCompleteAppearTranstion();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<E02Base> controller) {
        bool isTransition = controller.ObjectBase.E02Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E02AttackState.Instance, this);
        }

        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<E02Base> controller) {
    }
}
