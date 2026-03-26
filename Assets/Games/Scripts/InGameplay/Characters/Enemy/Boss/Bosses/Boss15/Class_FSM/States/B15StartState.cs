

using Class_FSM;
using UnityEngine;

public class B15StartState : B15State {
    #region Singleton
    public B15StartState() {

    }
    private static B15StartState instance = null;
    public static B15StartState Instance {
        get {
            if(instance == null) {
                instance = new B15StartState();
            }
            return instance;
        }
    }
    #endregion
    private B15Transition[] transitions = { B15CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B15Base> controller) {
    }

    protected override void DoStartActions(StateController<B15Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
