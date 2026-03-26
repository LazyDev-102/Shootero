using Class_FSM;
using UnityEngine;

public class MB13DeadState : MB13State {
    #region Singleton
    public MB13DeadState() {

    }
    private static MB13DeadState instance = null;
    public static MB13DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB13DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB13Base> controller) {
    }

    protected override void DoStartActions(StateController<MB13Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB13Base> controller) {
    }

    protected override Transition<MB13Base>[] GetTransitions() {
        return null;
    }
}
