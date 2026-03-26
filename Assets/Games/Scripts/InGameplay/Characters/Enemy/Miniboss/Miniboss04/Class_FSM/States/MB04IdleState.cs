using Class_FSM;
using UnityEngine;

public class MB04IdleState : MB04State {
    #region Singleton
    public MB04IdleState() {

    }
    private static MB04IdleState instance = null;
    public static MB04IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB04IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB04Transition[] transitions = { MB04CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB04Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB04Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB04Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB04Base>[] GetTransitions() {
        return transitions;
    }
}
