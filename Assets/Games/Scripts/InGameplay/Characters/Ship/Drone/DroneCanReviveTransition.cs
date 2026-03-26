

using Class_FSM;

public class DroneCanReviveTransition : Transition<DroneBase> {
    #region Singleton
    private DroneCanReviveTransition() {

    }
    private static DroneCanReviveTransition instance = null;
    public static DroneCanReviveTransition Instance {
        get {
            if (instance == null) {
                instance = new DroneCanReviveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<DroneBase> controller) {
        bool isTransition = true;
        if (isTransition) {
            controller.TransitionToState(DroneReviveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<DroneBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<DroneBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<DroneBase> controller) {
    }
}
