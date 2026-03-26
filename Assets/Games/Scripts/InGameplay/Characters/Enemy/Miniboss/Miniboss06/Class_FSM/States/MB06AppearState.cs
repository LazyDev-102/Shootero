using Class_FSM;
using UnityEngine;

public class MB06AppearState : MB06State {

    #region Singleton
    public MB06AppearState() {

    }
    private static MB06AppearState instance = null;
    public static MB06AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB06AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB06Transition[] transitions = { MB06AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB06Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB06Base> controller) {
        controller.ObjectBase.MB06Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB06Base> controller) {
    }

    protected override Transition<MB06Base>[] GetTransitions() {
        return transitions;
    }
}
