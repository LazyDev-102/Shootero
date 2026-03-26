

using Class_FSM;

public class ChestChip01CanMoveTransition : ChestChip01Transition {
    #region Singleton
    public ChestChip01CanMoveTransition() {

    }
    private static ChestChip01CanMoveTransition instance = null;
    public static ChestChip01CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ChestChip01Base> controller) {
        bool isTransition = controller.ObjectBase.ChestChip01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(ChestChip01MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ChestChip01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ChestChip01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ChestChip01Base> controller) {
    }
}
