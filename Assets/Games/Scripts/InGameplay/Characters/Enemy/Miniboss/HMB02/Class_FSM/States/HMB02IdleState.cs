using Class_FSM;
using UnityEngine;

public class HMB02IdleState : HMB02State {
    #region Singleton
    public HMB02IdleState() {

    }
    private static HMB02IdleState instance = null;
    public static HMB02IdleState Instance {
        get {
            if (instance == null) {
                instance = new HMB02IdleState();
            }
            return instance;
        }
    }
    #endregion

    private HMB02Transition[] transitions = { HMB02CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<HMB02Base> controller) {
    }

    protected override void DoUpdateActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<HMB02Base>[] GetTransitions() {
        return transitions;
    }
}
