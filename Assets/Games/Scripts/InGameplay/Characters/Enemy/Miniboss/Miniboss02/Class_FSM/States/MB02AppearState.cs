using Class_FSM;
using UnityEngine;

public class MB02AppearState : MB02State {

    #region Singleton
    public MB02AppearState() {

    }
    private static MB02AppearState instance = null;
    public static MB02AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB02AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB02Transition[] transitions = { MB02AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB02Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB02Base> controller) {
        controller.ObjectBase.MB02Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB02Base> controller) {
    }

    protected override Transition<MB02Base>[] GetTransitions() {
        return transitions;
    }
}
