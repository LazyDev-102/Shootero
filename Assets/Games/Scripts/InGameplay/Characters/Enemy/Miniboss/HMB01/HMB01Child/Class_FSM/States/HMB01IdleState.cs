using Class_FSM;
using UnityEngine;

public class HMB01IdleState : HMB01State {
    #region Singleton
    public HMB01IdleState() {

    }
    private static HMB01IdleState instance = null;
    public static HMB01IdleState Instance {
        get {
            if (instance == null) {
                instance = new HMB01IdleState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01Transition[] transitions = { HMB01CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<HMB01Base> controller) {
    }

    protected override void DoUpdateActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<HMB01Base>[] GetTransitions() {
        return transitions;
    }
}
