using Class_FSM;
using UnityEngine;

public class B05StartState : B05State {
    #region Singleton
    public B05StartState() {

    }
    private static B05StartState instance = null;
    public static B05StartState Instance {
        get {
            if(instance == null) {
                instance = new B05StartState();
            }
            return instance;
        }
    }
    #endregion
    private B05Transition[] transitions = { B05CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B05Base> controller) {
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
