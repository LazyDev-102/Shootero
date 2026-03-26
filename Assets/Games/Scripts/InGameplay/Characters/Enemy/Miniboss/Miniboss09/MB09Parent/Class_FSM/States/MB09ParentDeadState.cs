using Class_FSM;
using UnityEngine;

public class MB09ParentDeadState : MB09ParentState {
    #region Singleton
    public MB09ParentDeadState() {

    }
    private static MB09ParentDeadState instance = null;
    public static MB09ParentDeadState Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB09ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB09ParentBase> controller) {
    }

    protected override Transition<MB09ParentBase>[] GetTransitions() {
        return null;
    }
}
