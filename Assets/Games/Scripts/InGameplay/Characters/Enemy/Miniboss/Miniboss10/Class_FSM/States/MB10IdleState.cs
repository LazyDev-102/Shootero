using Class_FSM;
using UnityEngine;

public class MB10IdleState : MB10State {
    #region Singleton
    public MB10IdleState() {

    }
    private static MB10IdleState instance = null;
    public static MB10IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB10IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB10Transition[] transitions = { MB10CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB10Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB10Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB10Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB10Base>[] GetTransitions() {
        return transitions;
    }
}
