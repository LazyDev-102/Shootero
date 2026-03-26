using Class_FSM;
using UnityEngine;

public class MB15ChildAppearState : MB15ChildState {

    #region Singleton
    public MB15ChildAppearState() {

    }
    private static MB15ChildAppearState instance = null;
    public static MB15ChildAppearState Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildAppearState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ChildTransition[] transitions = { MB15ChildAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.MB15ChildMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<MB15ChildBase> controller) {
    }

    protected override Transition<MB15ChildBase>[] GetTransitions() {
        return transitions;
    }
}
