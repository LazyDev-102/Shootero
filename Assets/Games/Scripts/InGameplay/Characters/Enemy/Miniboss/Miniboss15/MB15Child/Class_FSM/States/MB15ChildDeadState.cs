using Class_FSM;
using UnityEngine;

public class MB15ChildDeadState : MB15ChildState {
    #region Singleton
    public MB15ChildDeadState() {

    }
    private static MB15ChildDeadState instance = null;
    public static MB15ChildDeadState Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB15ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB15ChildBase> controller) {
    }

    protected override Transition<MB15ChildBase>[] GetTransitions() {
        return null;
    }
}
