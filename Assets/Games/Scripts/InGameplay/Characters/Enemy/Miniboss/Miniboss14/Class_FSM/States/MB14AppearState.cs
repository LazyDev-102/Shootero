using Class_FSM;
using UnityEngine;

public class MB14AppearState : MB14State {

    #region Singleton
    public MB14AppearState() {

    }
    private static MB14AppearState instance = null;
    public static MB14AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB14AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB14Transition[] transitions = { MB14AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB14Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
        controller.ObjectBase.MB14Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return transitions;
    }
}
