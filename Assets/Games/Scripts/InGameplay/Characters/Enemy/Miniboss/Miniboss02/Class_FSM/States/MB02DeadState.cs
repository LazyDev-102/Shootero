using Class_FSM;
using UnityEngine;

public class MB02DeadState : MB02State {
    #region Singleton
    public MB02DeadState() {

    }
    private static MB02DeadState instance = null;
    public static MB02DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB02DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB02Base> controller) {
    }

    protected override void DoStartActions(StateController<MB02Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB02Base> controller) {
    }

    protected override Transition<MB02Base>[] GetTransitions() {
        return null;
    }
}
