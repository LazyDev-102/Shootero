using Class_FSM;

public class ChestChip01IsDieTransition : ChestChip01Transition {
    #region Singleton
    public ChestChip01IsDieTransition() {

    }
    private static ChestChip01IsDieTransition instance = null;
    public static ChestChip01IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<ChestChip01Base> controller) {
        bool isTransition = controller.ObjectBase.IsInitialized && controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(ChestChip01DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<ChestChip01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ChestChip01Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<ChestChip01Base> controller) {
    }
}
