using Class_FSM;

public class ToDieTurretTransition : Transition<TurretBase> {
    #region Singleton
    private ToDieTurretTransition() {

    }
    private static ToDieTurretTransition instance = null;
    public static ToDieTurretTransition Instance {
        get {
            if(instance == null) {
                instance = new ToDieTurretTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<TurretBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(DeadTurretState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<TurretBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<TurretBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<TurretBase> controller) {
    }
}