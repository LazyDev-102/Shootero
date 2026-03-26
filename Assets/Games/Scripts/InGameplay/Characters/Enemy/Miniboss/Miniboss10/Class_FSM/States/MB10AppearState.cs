using Class_FSM;
using UnityEngine;

public class MB10AppearState : MB10State {

    #region Singleton
    public MB10AppearState() {

    }
    private static MB10AppearState instance = null;
    public static MB10AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB10AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB10Transition[] transitions = { MB10AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB10Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB10Base> controller) {
        controller.ObjectBase.MB10Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB10Base> controller) {
    }

    protected override Transition<MB10Base>[] GetTransitions() {
        return transitions;
    }
}
