using Class_FSM;
using UnityEngine;

public class ME03B10StartState : ME03B10State {

    #region Singleton
    public ME03B10StartState() {

    }
    private static ME03B10StartState instance = null;
    public static ME03B10StartState Instance {
        get {
            if (instance == null) {
                instance = new ME03B10StartState();
            }
            return instance;
        }
    }
    #endregion

    private ME03B10Transition[] transitions = { ME03B10CanMoveTransition.Instance };

    protected override void DoEndActions(StateController<ME03B10Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B10Base> controller) {
        // controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<ME03B10Base> controller) {
    }

    protected override Transition<ME03B10Base>[] GetTransitions() {
        return transitions;
    }
}
