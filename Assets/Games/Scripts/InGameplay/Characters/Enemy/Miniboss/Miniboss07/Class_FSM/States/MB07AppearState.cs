using Class_FSM;
using UnityEngine;

public class MB07AppearState : MB07State {

    #region Singleton
    public MB07AppearState() {

    }
    private static MB07AppearState instance = null;
    public static MB07AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB07AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB07Transition[] transitions = { MB07AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB07Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB07Base> controller) {
        controller.ObjectBase.MB07Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB07Base> controller) {
    }

    protected override Transition<MB07Base>[] GetTransitions() {
        return transitions;
    }
}
