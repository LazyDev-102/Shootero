using Class_FSM;
using UnityEngine;

public class MB09DeadState : MB09State {
    #region Singleton
    public MB09DeadState() {

    }
    private static MB09DeadState instance = null;
    public static MB09DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB09DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB09Base> controller) {
    }

    protected override void DoStartActions(StateController<MB09Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB09Base> controller) {
    }

    protected override Transition<MB09Base>[] GetTransitions() {
        return null;
    }
}
