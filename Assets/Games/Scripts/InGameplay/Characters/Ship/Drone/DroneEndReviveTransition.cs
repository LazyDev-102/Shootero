

using Class_FSM;

public class DroneEndReviveTransition : Transition<DroneBase> {

    #region Singleton
    private DroneEndReviveTransition() {

    }
    private static DroneEndReviveTransition instance = null;
    public static DroneEndReviveTransition Instance {
        get {
            if (instance == null) {
                instance = new DroneEndReviveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<DroneBase> controller) {
        bool isTransition = controller.ObjectBase.gameObject.activeInHierarchy;
        if (isTransition) {
            controller.TransitionToState(IdleDroneState.Instance, this);
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
