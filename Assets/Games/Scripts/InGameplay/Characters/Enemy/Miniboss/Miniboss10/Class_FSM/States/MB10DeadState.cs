using Class_FSM;
using UnityEngine;

public class MB10DeadState : MB10State {
    #region Singleton
    public MB10DeadState() {

    }
    private static MB10DeadState instance = null;
    public static MB10DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB10DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB10Base> controller) {
    }

    protected override void DoStartActions(StateController<MB10Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB10Base> controller) {
    }

    protected override Transition<MB10Base>[] GetTransitions() {
        return null;
    }
}
