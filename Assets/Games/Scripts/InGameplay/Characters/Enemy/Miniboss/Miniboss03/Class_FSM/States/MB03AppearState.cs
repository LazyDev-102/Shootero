using Class_FSM;
using UnityEngine;

public class MB03AppearState : MB03State {

    #region Singleton
    public MB03AppearState() {

    }
    private static MB03AppearState instance = null;
    public static MB03AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB03AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB03Transition[] transitions = { MB03AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB03Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB03Base> controller) {
        controller.ObjectBase.MB03Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB03Base> controller) {
    }

    protected override Transition<MB03Base>[] GetTransitions() {
        return transitions;
    }
}
