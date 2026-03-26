using Class_FSM;
using UnityEngine;

public class MB06DeadState : MB06State {
    #region Singleton
    public MB06DeadState() {

    }
    private static MB06DeadState instance = null;
    public static MB06DeadState Instance {
        get {
            if (instance == null) {
                instance = new MB06DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<MB06Base> controller) {
    }

    protected override void DoStartActions(StateController<MB06Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MB06Base> controller) {
    }

    protected override Transition<MB06Base>[] GetTransitions() {
        return null;
    }
}
