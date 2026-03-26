using Class_FSM;
using UnityEngine;

public class HMB01DeadState : HMB01State {
    #region Singleton
    public HMB01DeadState() {

    }
    private static HMB01DeadState instance = null;
    public static HMB01DeadState Instance {
        get {
            if (instance == null) {
                instance = new HMB01DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<HMB01Base> controller) {
    }

    protected override void DoStartActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<HMB01Base> controller) {
    }

    protected override Transition<HMB01Base>[] GetTransitions() {
        return null;
    }
}
