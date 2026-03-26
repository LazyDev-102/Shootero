using Class_FSM;
using UnityEngine;

public class MB05IdleState : MB05State {
    #region Singleton
    public MB05IdleState() {

    }
    private static MB05IdleState instance = null;
    public static MB05IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB05IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB05Transition[] transitions = { MB05CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB05Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB05Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB05Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB05Base>[] GetTransitions() {
        return transitions;
    }
}
