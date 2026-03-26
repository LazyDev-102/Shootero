using Class_FSM;
using UnityEngine;

public class MB12AppearState : MB12State {

    #region Singleton
    public MB12AppearState() {

    }
    private static MB12AppearState instance = null;
    public static MB12AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB12AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB12Transition[] transitions = { MB12AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB12Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB12Base> controller) {
        controller.ObjectBase.MB12Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB12Base> controller) {
    }

    protected override Transition<MB12Base>[] GetTransitions() {
        return transitions;
    }
}
