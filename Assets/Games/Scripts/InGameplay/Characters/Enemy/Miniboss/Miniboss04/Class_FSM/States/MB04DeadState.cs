using Class_FSM;
using UnityEngine;

public class MB04DeadState : MB04State {

    #region Singleton
    public MB04DeadState() {

    }
    private static MB04DeadState instance = null;
    public static MB04DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB04DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<MB04Base> controller) {
    }

    protected override void DoStartActions(StateController<MB04Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB04Base> controller) {
    }

    protected override Transition<MB04Base>[] GetTransitions() {
        return null;
    }
}
