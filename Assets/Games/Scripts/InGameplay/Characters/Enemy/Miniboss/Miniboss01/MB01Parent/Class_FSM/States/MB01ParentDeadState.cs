using Class_FSM;
using UnityEngine;

public class MB01ParentDeadState : MB01ParentState {
    #region Singleton
    public MB01ParentDeadState() {

    }
    private static MB01ParentDeadState instance = null;
    public static MB01ParentDeadState Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB01ParentBase> controller) {
    }

    protected override Transition<MB01ParentBase>[] GetTransitions() {
        return null;
    }
}
