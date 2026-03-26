using Class_FSM;
using UnityEngine;

public class MB11AppearState : MB11State {

    #region Singleton
    public MB11AppearState() {

    }
    private static MB11AppearState instance = null;
    public static MB11AppearState Instance {
        get {
            if (instance == null) {
                instance = new MB11AppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB11Transition[] transitions = { MB11AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB11Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB11Base> controller) {
        controller.ObjectBase.MB11Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB11Base> controller) {
    }

    protected override Transition<MB11Base>[] GetTransitions() {
        return transitions;
    }
}
