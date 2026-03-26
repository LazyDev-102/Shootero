using Class_FSM;
using UnityEngine;

public class MB08AppearState : MB08State {

    #region Singleton
    public MB08AppearState() {

    }
    private static MB08AppearState instance = null;
    public static MB08AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB08AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB08Transition[] transitions = { MB08AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB08Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB08Base> controller) {
        controller.ObjectBase.MB08Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB08Base> controller) {
    }

    protected override Transition<MB08Base>[] GetTransitions() {
        return transitions;
    }
}
