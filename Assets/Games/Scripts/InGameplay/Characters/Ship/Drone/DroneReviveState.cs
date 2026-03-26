

using Class_FSM;

public class DroneReviveState : DroneState {
    #region Singleton
    private DroneReviveState() {

    }
    private static DroneReviveState instance = null;
    public static DroneReviveState Instance {
        get {
            if (instance == null) {
                instance = new DroneReviveState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<DroneBase>[] transitions = { DroneEndReviveTransition.Instance };
    protected override void DoEndActions(StateController<DroneBase> controller) {

    }

    protected override void DoStartActions(StateController<DroneBase> controller) {

    }

    protected override void DoUpdateActions(StateController<DroneBase> controller) {
    }

    protected override Transition<DroneBase>[] GetTransitions() {
        return transitions;
    }
}
