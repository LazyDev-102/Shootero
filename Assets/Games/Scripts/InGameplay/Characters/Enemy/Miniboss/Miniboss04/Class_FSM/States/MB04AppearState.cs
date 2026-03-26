using Class_FSM;
using UnityEngine;

public class MB04AppearState : MB04State {

    #region Singleton
    public MB04AppearState() {

    }
    private static MB04AppearState instance = null;
    public static MB04AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB04AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB04Transition[] transitions = { MB04AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB04Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB04Base> controller) {
        controller.ObjectBase.MB04Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB04Base> controller) {
    }

    protected override Transition<MB04Base>[] GetTransitions() {
        return transitions;
    }
}
