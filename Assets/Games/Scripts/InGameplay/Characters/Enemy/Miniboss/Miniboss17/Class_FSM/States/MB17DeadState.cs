using Class_FSM;
using UnityEngine;

public class MB17DeadState : MB17State {
    #region Singleton
    public MB17DeadState() {

    }
    private static MB17DeadState instance = null;
    public static MB17DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB17DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB17Base> controller) {
    }

    protected override void DoStartActions(StateController<MB17Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB17Base> controller) {
    }

    protected override Transition<MB17Base>[] GetTransitions() {
        return null;
    }
}
