using Class_FSM;
using UnityEngine;

public class HMB01ParentAppearState : HMB01ParentState {

    #region Singleton
    public HMB01ParentAppearState() {

    }
    private static HMB01ParentAppearState instance = null;
    public static HMB01ParentAppearState Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentAppearState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01ParentTransition[] transitions = { HMB01ParentAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.MinibossMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<HMB01ParentBase> controller) {
    }

    protected override Transition<HMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
