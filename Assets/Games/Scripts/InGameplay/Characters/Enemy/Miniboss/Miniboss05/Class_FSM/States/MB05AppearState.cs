using Class_FSM;
using UnityEngine;

public class MB05AppearState : MB05State {

    #region Singleton
    public MB05AppearState() {

    }
    private static MB05AppearState instance = null;
    public static MB05AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB05AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB05Transition[] transitions = { MB05AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB05Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB05Base> controller) {
        controller.ObjectBase.MB05Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB05Base> controller) {
    }

    protected override Transition<MB05Base>[] GetTransitions() {
        return transitions;
    }
}
