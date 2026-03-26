using Class_FSM;
using UnityEngine;

public class HMB02DeadState : HMB02State {
    #region Singleton
    public HMB02DeadState() {

    }
    private static HMB02DeadState instance = null;
    public static HMB02DeadState Instance {
        get {
            if (instance == null) {
                instance = new HMB02DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<HMB02Base> controller) {
    }

    protected override void DoStartActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<HMB02Base> controller) {
    }

    protected override Transition<HMB02Base>[] GetTransitions() {
        return null;
    }
}
