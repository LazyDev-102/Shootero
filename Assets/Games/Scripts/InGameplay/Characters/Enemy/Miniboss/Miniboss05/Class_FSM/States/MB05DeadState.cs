using Class_FSM;
using UnityEngine;

public class MB05DeadState : MB05State {
    #region Singleton
    public MB05DeadState() {

    }
    private static MB05DeadState instance = null;
    public static MB05DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB05DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB05Base> controller) {
    }

    protected override void DoStartActions(StateController<MB05Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB05Base> controller) {
    }

    protected override Transition<MB05Base>[] GetTransitions() {
        return null;
    }
}
