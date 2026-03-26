using Class_FSM;
using UnityEngine;

public class MB12DeadState : MB12State {
    #region Singleton
    public MB12DeadState() {

    }
    private static MB12DeadState instance = null;
    public static MB12DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB12DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB12Base> controller) {
    }

    protected override void DoStartActions(StateController<MB12Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB12Base> controller) {
    }

    protected override Transition<MB12Base>[] GetTransitions() {
        return null;
    }
}
