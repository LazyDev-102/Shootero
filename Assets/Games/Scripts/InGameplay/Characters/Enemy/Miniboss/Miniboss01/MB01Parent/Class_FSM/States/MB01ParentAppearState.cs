using Class_FSM;
using UnityEngine;

public class MB01ParentAppearState : MB01ParentState {

    #region Singleton
    public MB01ParentAppearState() {

    }
    private static MB01ParentAppearState instance = null;
    public static MB01ParentAppearState Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentAppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB01ParentTransition[] transitions = { MB01ParentAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.MB01ParentMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB01ParentBase> controller) {
    }

    protected override Transition<MB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
