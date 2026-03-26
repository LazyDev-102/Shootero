using Class_FSM;
using UnityEngine;

public class MB17IdleState : MB17State {
    #region Singleton
    public MB17IdleState() {

    }
    private static MB17IdleState instance = null;
    public static MB17IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB17IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB17Transition[] transitions = { MB17CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB17Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB17Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB17Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB17Base>[] GetTransitions() {
        return transitions;
    }
}
