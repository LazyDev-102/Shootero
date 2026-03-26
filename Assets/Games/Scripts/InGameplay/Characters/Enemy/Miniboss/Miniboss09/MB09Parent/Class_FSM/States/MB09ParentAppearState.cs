using Class_FSM;
using UnityEngine;

public class MB09ParentAppearState : MB09ParentState {

    #region Singleton
    public MB09ParentAppearState() {

    }
    private static MB09ParentAppearState instance = null;
    public static MB09ParentAppearState Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentAppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB09ParentTransition[] transitions = { MB09ParentAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.MB09ParentMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB09ParentBase> controller) {
    }

    protected override Transition<MB09ParentBase>[] GetTransitions() {
        return transitions;
    }
}
