

using Class_FSM;

public class HB01CanAttackTransition : HB01Transition {
    #region Singleton
    public HB01CanAttackTransition() {

    }
    private static HB01CanAttackTransition instance = null;
    public static HB01CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new HB01CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.HB01Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(HB01AttackState.Instance, this);
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
