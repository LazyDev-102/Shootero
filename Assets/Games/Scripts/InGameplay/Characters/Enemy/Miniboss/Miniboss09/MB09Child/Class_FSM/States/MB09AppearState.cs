using Class_FSM;
using UnityEngine;

public class MB09AppearState : MB09State {

    #region Singleton
    public MB09AppearState() {

    }
    private static MB09AppearState instance = null;
    public static MB09AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB09AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB09Transition[] transitions = { MB09AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB09Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB09Base> controller) {
        controller.ObjectBase.MB09Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB09Base> controller) {
    }

    protected override Transition<MB09Base>[] GetTransitions() {
        return transitions;
    }
}
