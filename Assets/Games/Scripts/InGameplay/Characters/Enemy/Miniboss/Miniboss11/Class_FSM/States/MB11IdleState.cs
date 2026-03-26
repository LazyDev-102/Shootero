using Class_FSM;
using UnityEngine;

public class MB11IdleState : MB11State {
    #region Singleton
    public MB11IdleState() {

    }
    private static MB11IdleState instance = null;
    public static MB11IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB11IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB11Transition[] transitions = { MB11CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB11Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB11Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB11Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB11Base>[] GetTransitions() {
        return transitions;
    }
}
