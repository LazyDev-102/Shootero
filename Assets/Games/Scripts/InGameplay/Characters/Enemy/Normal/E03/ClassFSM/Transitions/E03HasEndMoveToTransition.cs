

using Class_FSM;

public class E03HasEndMoveToTransition : E03Transition{
    #region Singleton
    public E03HasEndMoveToTransition() {

    }
    private static E03HasEndMoveToTransition instance = null;
    public static E03HasEndMoveToTransition Instance {
        get {
            if(instance == null) {
                instance = new E03HasEndMoveToTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<E03Base> controller) {
        bool isTransition = controller.ObjectBase.E03Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E03AimState.Instance, this);
        }

        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<E03Base> controller) {
    }
}
