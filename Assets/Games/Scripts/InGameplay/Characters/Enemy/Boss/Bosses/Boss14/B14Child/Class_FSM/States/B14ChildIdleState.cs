using Class_FSM;
using UnityEngine;

public class B14ChildIdleState : B14ChildState {
    #region Singleton
    public B14ChildIdleState() {

    }
    private static B14ChildIdleState instance = null;
    public static B14ChildIdleState Instance {
        get {
            if (instance == null) {
                instance = new B14ChildIdleState();
            }
            return instance;
        }
    }
    #endregion

    //private B14ChildTransition[] transitions = { B14ChildCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoUpdateActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B14ChildBase>[] GetTransitions() {
        return null;
    }
}
