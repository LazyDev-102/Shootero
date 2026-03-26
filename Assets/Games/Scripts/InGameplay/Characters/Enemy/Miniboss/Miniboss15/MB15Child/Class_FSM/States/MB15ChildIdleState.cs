using Class_FSM;
using UnityEngine;

public class MB15ChildIdleState : MB15ChildState {
    #region Singleton
    public MB15ChildIdleState() {

    }
    private static MB15ChildIdleState instance = null;
    public static MB15ChildIdleState Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildIdleState();
            }
            return instance;
        }
    }
    #endregion

    //private MB15ChildTransition[] transitions = { MB15ChildCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoUpdateActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB15ChildBase>[] GetTransitions() {
        return null;
    }
}
