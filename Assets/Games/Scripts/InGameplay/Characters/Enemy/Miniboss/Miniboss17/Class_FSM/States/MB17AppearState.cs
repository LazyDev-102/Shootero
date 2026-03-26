using Class_FSM;
using UnityEngine;

public class MB17AppearState : MB17State {

    #region Singleton
    public MB17AppearState() {

    }
    private static MB17AppearState instance = null;
    public static MB17AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB17AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB17Transition[] transitions = { MB17AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB17Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB17Base> controller) {
        controller.ObjectBase.MB17Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB17Base> controller) {
    }

    protected override Transition<MB17Base>[] GetTransitions() {
        return transitions;
    }
}
