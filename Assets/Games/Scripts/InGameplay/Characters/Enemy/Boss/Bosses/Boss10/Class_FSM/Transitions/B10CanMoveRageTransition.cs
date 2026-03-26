using Class_FSM;
using UnityEngine;

public class B10CanMoveRageTransition : B10Transition {
    #region Singleton
    public B10CanMoveRageTransition() {

    }
    private static B10CanMoveRageTransition instance = null;
    public static B10CanMoveRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B10CanMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = controller.ObjectBase.B10Move.CanMoveRage();
        if (isTransition) {
            controller.TransitionToState(B10MoveRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
