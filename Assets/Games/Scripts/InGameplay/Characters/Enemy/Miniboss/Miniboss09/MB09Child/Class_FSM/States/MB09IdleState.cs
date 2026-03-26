using Class_FSM;
using UnityEngine;

public class MB09IdleState : MB09State {
    #region Singleton
    public MB09IdleState() {

    }
    private static MB09IdleState instance = null;
    public static MB09IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB09IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB09Transition[] transitions = { MB09CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB09Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB09Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB09Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB09Base>[] GetTransitions() {
        return transitions;
    }
}
