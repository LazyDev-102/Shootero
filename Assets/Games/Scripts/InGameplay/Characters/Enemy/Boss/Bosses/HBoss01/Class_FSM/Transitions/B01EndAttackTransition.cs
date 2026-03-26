

using Class_FSM;

public class HB01EndAttackTransition : HB01Transition {
    #region Singleton
    public HB01EndAttackTransition() {

    }
    private static HB01EndAttackTransition instance = null;
    public static HB01EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HB01EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = !controller.ObjectBase.HB01Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(HB01MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HB01Base> controller) {
    }
}
