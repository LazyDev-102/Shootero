using Class_FSM;
using UnityEngine;

public class B11StartState : B11State {
    #region Singleton
    public B11StartState() {

    }
    private static B11StartState instance = null;
    public static B11StartState Instance {
        get {
            if(instance == null) {
                instance = new B11StartState();
            }
            return instance;
        }
    }
    #endregion
    private B11Transition[] transitions = { B11CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B11Base> controller) {
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
