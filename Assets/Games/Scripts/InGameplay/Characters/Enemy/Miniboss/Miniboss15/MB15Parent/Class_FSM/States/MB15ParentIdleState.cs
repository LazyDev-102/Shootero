using Class_FSM;
using UnityEngine;

public class MB15ParentIdleState : MB15ParentState {
    #region Singleton
    public MB15ParentIdleState() {

    }
    private static MB15ParentIdleState instance = null;
    public static MB15ParentIdleState Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentIdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ParentTransition[] transitions = { MB15ParentCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB15ParentBase> controller) {
    }

    protected override void DoUpdateActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB15ParentBase>[] GetTransitions() {
        return transitions;
    }
}
