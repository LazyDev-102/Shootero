using Class_FSM;
using UnityEngine;

public class MB08IdleState : MB08State {
    #region Singleton
    public MB08IdleState() {

    }
    private static MB08IdleState instance = null;
    public static MB08IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB08IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB08Transition[] transitions = { MB08CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB08Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB08Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB08Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB08Base>[] GetTransitions() {
        return transitions;
    }
}
