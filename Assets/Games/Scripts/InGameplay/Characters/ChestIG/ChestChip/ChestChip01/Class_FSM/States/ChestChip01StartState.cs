

using Class_FSM;

public class ChestChip01StartState : ChestChip01State {
    #region Singleton
    public ChestChip01StartState() {

    }
    private static ChestChip01StartState instance = null;
    public static ChestChip01StartState Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01StartState();
            }
            return instance;
        }
    }
    #endregion

    private ChestChip01Transition[] transitions = { ChestChip01CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<ChestChip01Base> controller) {
    }

    protected override void DoStartActions(StateController<ChestChip01Base> controller) {
        controller.ObjectBase.Spawn();
        controller.ObjectBase.SpawnParts();
    }

    protected override void DoUpdateActions(StateController<ChestChip01Base> controller) {
    }

    protected override Transition<ChestChip01Base>[] GetTransitions() {
        return transitions;
    }
}
