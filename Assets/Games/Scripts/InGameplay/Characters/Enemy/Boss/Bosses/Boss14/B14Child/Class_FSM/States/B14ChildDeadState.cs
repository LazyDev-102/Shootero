using Class_FSM;
using UnityEngine;

public class B14ChildDeadState : B14ChildState {
    #region Singleton
    public B14ChildDeadState() {

    }
    private static B14ChildDeadState instance = null;
    public static B14ChildDeadState Instance {
        get {
            if (instance == null) {
                instance = new B14ChildDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<B14ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B14ChildBase> controller) {
    }

    protected override Transition<B14ChildBase>[] GetTransitions() {
        return null;
    }
}
