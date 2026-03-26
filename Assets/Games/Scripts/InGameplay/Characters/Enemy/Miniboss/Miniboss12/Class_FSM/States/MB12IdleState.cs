using Class_FSM;
using UnityEngine;

public class MB12IdleState : MB12State {
    #region Singleton
    public MB12IdleState() {

    }
    private static MB12IdleState instance = null;
    public static MB12IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB12IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB12Transition[] transitions = { MB12CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB12Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB12Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB12Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB12Base>[] GetTransitions() {
        return transitions;
    }
}
