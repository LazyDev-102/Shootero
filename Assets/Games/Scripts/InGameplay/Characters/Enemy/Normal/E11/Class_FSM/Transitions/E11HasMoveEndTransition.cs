

using Class_FSM;

public class E11HasMoveEndTransition : E11Transition {
    #region Singleton
    public E11HasMoveEndTransition() {

    }
    private static E11HasMoveEndTransition instance = null;
    public static E11HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E11HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E11Base> controller) {
        bool isTransition = controller.ObjectBase.E11Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E11AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E11Base> controller) {
    }
}
