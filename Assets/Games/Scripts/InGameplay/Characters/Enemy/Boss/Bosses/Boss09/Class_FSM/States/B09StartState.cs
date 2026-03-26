using Class_FSM;
using UnityEngine;

public class B09StartState : B09State {
    #region Singleton
    public B09StartState() {

    }
    private static B09StartState instance = null;
    public static B09StartState Instance {
        get {
            if(instance == null) {
                instance = new B09StartState();
            }
            return instance;
        }
    }
    #endregion
    private B09Transition[] transitions = { B09CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B09Base> controller) {
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}
