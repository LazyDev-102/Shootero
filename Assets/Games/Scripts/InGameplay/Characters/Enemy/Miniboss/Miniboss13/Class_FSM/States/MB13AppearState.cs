using Class_FSM;
using UnityEngine;

public class MB13AppearState : MB13State {

    #region Singleton
    public MB13AppearState() {

    }
    private static MB13AppearState instance = null;
    public static MB13AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB13AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB13Transition[] transitions = { MB13AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB13Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB13Base> controller) {
        controller.ObjectBase.MB13Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB13Base> controller) {
    }

    protected override Transition<MB13Base>[] GetTransitions() {
        return transitions;
    }
}
