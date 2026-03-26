

using Class_FSM;

public class ChestChip01DestroyState : ChestChip01State {
    #region Singleton
    public ChestChip01DestroyState() {

    }
    private static ChestChip01DestroyState instance = null;
    public static ChestChip01DestroyState Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01DestroyState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<ChestChip01Base> controller) {
    }

    protected override void DoStartActions(StateController<ChestChip01Base> controller) {
        controller.ObjectBase.Despawn();
    }

    protected override void DoUpdateActions(StateController<ChestChip01Base> controller) {
    }

    protected override Transition<ChestChip01Base>[] GetTransitions() {
        return null;
    }
}
