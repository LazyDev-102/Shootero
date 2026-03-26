using Class_FSM;
using UnityEngine;

public class MB01AppearState : MB01State {

    #region Singleton
    public MB01AppearState() {

    }
    private static MB01AppearState instance = null;
    public static MB01AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB01AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB01Transition[] transitions = { MB01AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB01Base> controller) {
        controller.ObjectBase.MB01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB01Base> controller) {
    }

    protected override Transition<MB01Base>[] GetTransitions() {
        return transitions;
    }
}
