using Class_FSM;
using UnityEngine;

public class ME03B10DeadState : ME03B10State {
    #region Singleton
    public ME03B10DeadState() {

    }
    private static ME03B10DeadState instance = null;
    public static ME03B10DeadState Instance {
        get {
            if (instance == null) {
                instance = new ME03B10DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<ME03B10Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B10Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<ME03B10Base> controller) {
    }

    protected override Transition<ME03B10Base>[] GetTransitions() {
        return null;
    }
}
