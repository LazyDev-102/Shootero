using Class_FSM;
using UnityEngine;

public class MB07IdleState : MB07State {
    #region Singleton
    public MB07IdleState() {

    }
    private static MB07IdleState instance = null;
    public static MB07IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB07IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB07Transition[] transitions = { MB07CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB07Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB07Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB07Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB07Base>[] GetTransitions() {
        return transitions;
    }
}
