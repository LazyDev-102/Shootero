using Class_FSM;
using UnityEngine;

public class MB15ParentDeadState : MB15ParentState {
    #region Singleton
    public MB15ParentDeadState() {

    }
    private static MB15ParentDeadState instance = null;
    public static MB15ParentDeadState Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB15ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB15ParentBase> controller) {
    }

    protected override Transition<MB15ParentBase>[] GetTransitions() {
        return null;
    }
}
