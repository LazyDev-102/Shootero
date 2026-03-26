using Class_FSM;
using UnityEngine;

public class MB03DeadState : MB03State {
    #region Singleton
    public MB03DeadState() {

    }
    private static MB03DeadState instance = null;
    public static MB03DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB03DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB03Base> controller) {
    }

    protected override void DoStartActions(StateController<MB03Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB03Base> controller) {
    }

    protected override Transition<MB03Base>[] GetTransitions() {
        return null;
    }
}
