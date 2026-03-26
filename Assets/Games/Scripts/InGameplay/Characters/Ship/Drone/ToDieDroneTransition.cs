using Class_FSM;

public class ToDieDroneTransition : Transition<DroneBase> {
    #region Singleton
    private ToDieDroneTransition() {

    }
    private static ToDieDroneTransition instance = null;
    public static ToDieDroneTransition Instance {
        get {
            if(instance == null) {
                instance = new ToDieDroneTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<DroneBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(DeadDroneState.Instance, this);
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