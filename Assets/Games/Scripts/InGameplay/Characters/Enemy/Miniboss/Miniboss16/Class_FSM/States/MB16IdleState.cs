using Class_FSM;
using UnityEngine;

public class MB16IdleState : MB16State {
    #region Singleton
    public MB16IdleState() {

    }
    private static MB16IdleState instance = null;
    public static MB16IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB16IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB16Transition[] transitions = { MB16CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB16Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB16Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB16Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB16Base>[] GetTransitions() {
        return transitions;
    }
}
