using Class_FSM;
using UnityEngine;

public class MB16AppearState : MB16State {

    #region Singleton
    public MB16AppearState() {

    }
    private static MB16AppearState instance = null;
    public static MB16AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB16AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB16Transition[] transitions = { MB16AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB16Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB16Base> controller) {
        controller.ObjectBase.MB16Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB16Base> controller) {
    }

    protected override Transition<MB16Base>[] GetTransitions() {
        return transitions;
    }
}
