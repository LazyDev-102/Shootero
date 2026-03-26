using Class_FSM;
using UnityEngine;

public class MB09ParentIdleState : MB09ParentState {
    #region Singleton
    public MB09ParentIdleState() {

    }
    private static MB09ParentIdleState instance = null;
    public static MB09ParentIdleState Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentIdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB09ParentTransition[] transitions = { MB09ParentCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB09ParentBase> controller) {
    }

    protected override void DoUpdateActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB09ParentBase>[] GetTransitions() {
        return transitions;
    }
}
