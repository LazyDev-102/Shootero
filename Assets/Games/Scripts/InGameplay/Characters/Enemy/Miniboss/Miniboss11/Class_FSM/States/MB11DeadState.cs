using Class_FSM;
using UnityEngine;

public class MB11DeadState : MB11State {
    #region Singleton
    public MB11DeadState() {

    }
    private static MB11DeadState instance = null;
    public static MB11DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB11DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB11Base> controller) {
    }

    protected override void DoStartActions(StateController<MB11Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB11Base> controller) {
    }

    protected override Transition<MB11Base>[] GetTransitions() {
        return null;
    }
}
