using Class_FSM;
using UnityEngine;

public class MB01IdleState : MB01State {
    #region Singleton
    public MB01IdleState() {

    }
    private static MB01IdleState instance = null;
    public static MB01IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB01IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB01Transition[] transitions = { MB01CanAppearTransition.Instance };

    protected override void DoEndActions(StateController<MB01Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB01Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoUpdateActions(StateController<MB01Base> controller) {
        //controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB01Base>[] GetTransitions() {
        return transitions;
    }
}
