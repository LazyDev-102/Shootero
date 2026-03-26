using Class_FSM;
using UnityEngine;

public class B10StartRageState : B10State {
    #region Singleton
    public B10StartRageState() {

    }
    private static B10StartRageState instance = null;
    public static B10StartRageState Instance {
        get {
            if (instance == null) {
                instance = new B10StartRageState();
            }
            return instance;
        }
    }
    #endregion


    private B10Transition[] transitions = { B10CanMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
