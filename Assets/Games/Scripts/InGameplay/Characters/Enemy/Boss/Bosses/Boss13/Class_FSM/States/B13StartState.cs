using Class_FSM;
using UnityEngine;

public class B13StartState : B13State {
    #region Singleton
    public B13StartState() {

    }
    private static B13StartState instance = null;
    public static B13StartState Instance {
        get {
            if (instance == null) {
                instance = new B13StartState();
            }
            return instance;
        }
    }
    #endregion
    private B13Transition[] transitions = { B13CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B13Base> controller) {
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
