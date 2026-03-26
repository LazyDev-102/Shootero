using Class_FSM;
using UnityEngine;

public class MB13IdleState : MB13State {
    #region Singleton
    public MB13IdleState() {

    }
    private static MB13IdleState instance = null;
    public static MB13IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB13IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB13Transition[] transitions = { MB13CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB13Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB13Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB13Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB13Base>[] GetTransitions() {
        return transitions;
    }
}
