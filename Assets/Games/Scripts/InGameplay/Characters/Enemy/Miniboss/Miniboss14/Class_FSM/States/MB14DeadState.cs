using Class_FSM;
using UnityEngine;

public class MB14DeadState : MB14State {
    #region Singleton
    public MB14DeadState() {

    }
    private static MB14DeadState instance = null;
    public static MB14DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB14DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB14Base> controller) {
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return null;
    }
}
