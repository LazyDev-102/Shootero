using Class_FSM;
using UnityEngine;

public class B10MoveRageState : B10State {

    #region Singleton
    public B10MoveRageState() {

    }
    private static B10MoveRageState instance = null;
    public static B10MoveRageState Instance {
        get {
            if (instance == null) {
                instance = new B10MoveRageState();
            }
            return instance;
        }
    }
    #endregion

    private B10Transition[] transitions = { B10EndMoveRageTransition.Instance };

    protected override void DoEndActions(StateController<B10Base> controller) {

    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Move.StartMoveRage();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {

    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
