using Class_FSM;
using UnityEngine;

public class MB07DeadState : MB07State {
    #region Singleton
    public MB07DeadState() {

    }
    private static MB07DeadState instance = null;
    public static MB07DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB07DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB07Base> controller) {
    }

    protected override void DoStartActions(StateController<MB07Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB07Base> controller) {
    }

    protected override Transition<MB07Base>[] GetTransitions() {
        return null;
    }
}
