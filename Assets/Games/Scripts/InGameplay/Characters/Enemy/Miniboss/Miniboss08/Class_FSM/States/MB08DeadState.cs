using Class_FSM;
using UnityEngine;

public class MB08DeadState : MB08State {

    #region Singleton
    public MB08DeadState() {

    }
    private static MB08DeadState instance = null;
    public static MB08DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB08DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<MB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MB08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB08Base> controller) {
    }

    protected override Transition<MB08Base>[] GetTransitions() {
        return null;
    }
}
