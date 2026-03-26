using Class_FSM;
using UnityEngine;

public class ME03B10IdleState : ME03B10State {

    #region Singleton
    public ME03B10IdleState() {

    }
    private static ME03B10IdleState instance = null;
    public static ME03B10IdleState Instance {
        get {
            if (instance == null) {
                instance = new ME03B10IdleState();
            }
            return instance;
        }
    }
    #endregion

    private ME03B10Transition[] transitons = { ME03B10CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<ME03B10Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B10Base> controller) {
    }

    protected override void DoUpdateActions(StateController<ME03B10Base> controller) {
    }

    protected override Transition<ME03B10Base>[] GetTransitions() {
        return transitons;
    }
}
