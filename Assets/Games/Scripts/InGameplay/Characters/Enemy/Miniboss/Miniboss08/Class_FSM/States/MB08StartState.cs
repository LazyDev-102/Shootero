using Class_FSM;
using UnityEngine;

public class MB08StartState : MB08State {
    #region Singleton
    public MB08StartState() {

    }
    private static MB08StartState instance = null;
    public static MB08StartState Instance {
        get {
            if (instance == null) {
                instance = new MB08StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB08Transition[] transitions = { MB08CanAppearTransition.Instance };

    protected override void DoEndActions(StateController<MB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MB08Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB08Base> controller) {
    }

    protected override Transition<MB08Base>[] GetTransitions() {
        return transitions;
    }
}
