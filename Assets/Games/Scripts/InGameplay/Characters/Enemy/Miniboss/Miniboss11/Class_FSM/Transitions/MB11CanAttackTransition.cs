using Class_FSM;
using UnityEngine;

public class MB11CanAttackTransition : MB11Transition {

    #region Singleton
    public MB11CanAttackTransition() {

    }
    private static MB11CanAttackTransition instance = null;
    public static MB11CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB11CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB11Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB11Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB11AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB11Base> controller) {
    }
}
