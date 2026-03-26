using Class_FSM;
using UnityEngine;

public class MB03IdleState : MB03State {
    #region Singleton
    public MB03IdleState() {

    }
    private static MB03IdleState instance = null;
    public static MB03IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB03IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB03Transition[] transitions = { MB03CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB03Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB03Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB03Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB03Base>[] GetTransitions() {
        return transitions;
    }
}
