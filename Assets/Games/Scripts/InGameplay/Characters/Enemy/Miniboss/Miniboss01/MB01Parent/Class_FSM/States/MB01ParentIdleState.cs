using Class_FSM;
using UnityEngine;

public class MB01ParentIdleState : MB01ParentState {
    #region Singleton
    public MB01ParentIdleState() {

    }
    private static MB01ParentIdleState instance = null;
    public static MB01ParentIdleState Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentIdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB01ParentTransition[] transitions = { MB01ParentCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB01ParentBase> controller) {
    }

    protected override void DoUpdateActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
