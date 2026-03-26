using Class_FSM;
using UnityEngine;

public class B12StartState : B12State {
    #region Singleton
    public B12StartState() {

    }
    private static B12StartState instance = null;
    public static B12StartState Instance {
        get {
            if(instance == null) {
                instance = new B12StartState();
            }
            return instance;
        }
    }
    #endregion
    private B12Transition[] transitions = { B12CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B12Base> controller) {
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
