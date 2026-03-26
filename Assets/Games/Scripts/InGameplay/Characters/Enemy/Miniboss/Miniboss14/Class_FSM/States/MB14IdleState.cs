using Class_FSM;
using UnityEngine;

public class MB14IdleState : MB14State {
    #region Singleton
    public MB14IdleState() {

    }
    private static MB14IdleState instance = null;
    public static MB14IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB14IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB14Transition[] transitions = { MB14CanAttackTransition.Instance, MB14CanSpecialTransition.Instance };

    protected override void DoEndActions(StateController<MB14Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return transitions;
    }
}
