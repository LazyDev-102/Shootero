

using Class_FSM;

public class ChestChip01OutBoundTransition : ChestChip01Transition {
    #region Singleton
    public ChestChip01OutBoundTransition() {

    }
    private static ChestChip01OutBoundTransition instance = null;
    public static ChestChip01OutBoundTransition Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01OutBoundTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ChestChip01Base> controller) {
        bool isTransition = controller.ObjectBase.ChestChip01Move.HasOutBorder();
        if (isTransition) {
            controller.TransitionToState(ChestChip01DestroyState.Instance, this);
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
