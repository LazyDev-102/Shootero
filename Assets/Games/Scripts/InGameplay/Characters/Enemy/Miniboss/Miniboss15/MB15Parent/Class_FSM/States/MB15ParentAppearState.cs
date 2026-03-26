using Class_FSM;
using UnityEngine;

public class MB15ParentAppearState : MB15ParentState {

    #region Singleton
    public MB15ParentAppearState() {

    }
    private static MB15ParentAppearState instance = null;
    public static MB15ParentAppearState Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentAppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ParentTransition[] transitions = { MB15ParentAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.MB15ParentMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB15ParentBase> controller) {
    }

    protected override Transition<MB15ParentBase>[] GetTransitions() {
        return transitions;
    }
}
