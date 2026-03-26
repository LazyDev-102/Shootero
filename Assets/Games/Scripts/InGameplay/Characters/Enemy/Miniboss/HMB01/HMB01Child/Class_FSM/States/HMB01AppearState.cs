using Class_FSM;
using UnityEngine;

public class HMB01AppearState : HMB01State {

    #region Singleton
    public HMB01AppearState() {

    }
    private static HMB01AppearState instance = null;
    public static HMB01AppearState Instance {
        get {
            if (instance == null) {
                instance = new HMB01AppearState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01Transition[] transitions = { HMB01AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.HMB01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<HMB01Base> controller) {
    }

    protected override Transition<HMB01Base>[] GetTransitions() {
        return transitions;
    }
}
