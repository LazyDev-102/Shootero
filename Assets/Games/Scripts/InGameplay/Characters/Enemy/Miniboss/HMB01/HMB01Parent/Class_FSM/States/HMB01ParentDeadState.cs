using Class_FSM;
using UnityEngine;

public class HMB01ParentDeadState : HMB01ParentState {
    #region Singleton
    public HMB01ParentDeadState() {

    }
    private static HMB01ParentDeadState instance = null;
    public static HMB01ParentDeadState Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<HMB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<HMB01ParentBase> controller) {
    }

    protected override Transition<HMB01ParentBase>[] GetTransitions() {
        return null;
    }
}
