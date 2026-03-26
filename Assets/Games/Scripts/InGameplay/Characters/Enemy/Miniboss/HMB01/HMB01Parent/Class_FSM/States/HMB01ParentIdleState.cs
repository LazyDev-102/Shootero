using Class_FSM;
using UnityEngine;

public class HMB01ParentIdleState : HMB01ParentState {
    #region Singleton
    public HMB01ParentIdleState() {

    }
    private static HMB01ParentIdleState instance = null;
    public static HMB01ParentIdleState Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentIdleState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01ParentTransition[] transitions = { HMB01ParentCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<HMB01ParentBase> controller) {
    }

    protected override void DoUpdateActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<HMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
