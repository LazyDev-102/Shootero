using Class_FSM;
using UnityEngine;

public class MB04StartState : MB04State {
    #region Singleton
    public MB04StartState() {

    }
    private static MB04StartState instance = null;
    public static MB04StartState Instance {
        get {
            if (instance == null) {
                instance = new MB04StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB04Transition[] transitions = { MB04CanAppearTransition.Instance };

    protected override void DoEndActions(StateController<MB04Base> controller) {
    }

    protected override void DoStartActions(StateController<MB04Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB04Base> controller) {
    }

    protected override Transition<MB04Base>[] GetTransitions() {
        return transitions;
    }
}
