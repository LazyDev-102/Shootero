using Class_FSM;
using UnityEngine;

public class MB01DeadState : MB01State {
    #region Singleton
    public MB01DeadState() {

    }
    private static MB01DeadState instance = null;
    public static MB01DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB01DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB01Base> controller) {
    }

    protected override void DoStartActions(StateController<MB01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB01Base> controller) {
    }

    protected override Transition<MB01Base>[] GetTransitions() {
        return null;
    }
}
