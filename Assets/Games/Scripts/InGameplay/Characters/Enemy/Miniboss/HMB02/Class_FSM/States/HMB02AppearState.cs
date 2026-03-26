using Class_FSM;
using UnityEngine;

public class HMB02AppearState : HMB02State {

    #region Singleton
    public HMB02AppearState() {

    }
    private static HMB02AppearState instance = null;
    public static HMB02AppearState Instance {
        get {
            if (instance == null) {
                instance = new HMB02AppearState();
            }
            return instance;
        }
    }
    #endregion

    private HMB02Transition[] transitions = { HMB02AppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.HMB02Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<HMB02Base> controller) {
    }

    protected override Transition<HMB02Base>[] GetTransitions() {
        return transitions;
    }
}
