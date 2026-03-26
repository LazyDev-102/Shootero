using Class_FSM;
using UnityEngine;

public class MB02IdleState : MB02State {
    #region Singleton
    public MB02IdleState() {

    }
    private static MB02IdleState instance = null;
    public static MB02IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB02IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB02Transition[] transitions = { MB02CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB02Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB02Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB02Base>[] GetTransitions() {
        return transitions;
    }
}
