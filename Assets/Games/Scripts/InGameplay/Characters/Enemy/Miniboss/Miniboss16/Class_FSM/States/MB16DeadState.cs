using Class_FSM;
using UnityEngine;

public class MB16DeadState : MB16State {
    #region Singleton
    public MB16DeadState() {

    }
    private static MB16DeadState instance = null;
    public static MB16DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB16DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB16Base> controller) {
    }

    protected override void DoStartActions(StateController<MB16Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB16Base> controller) {
    }

    protected override Transition<MB16Base>[] GetTransitions() {
        return null;
    }
}
