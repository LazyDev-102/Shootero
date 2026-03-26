using Class_FSM;
using UnityEngine;

public class B06StartState : B06State {
    #region Singleton
    public B06StartState() {

    }
    private static B06StartState instance = null;
    public static B06StartState Instance {
        get {
            if(instance == null) {
                instance = new B06StartState();
            }
            return instance;
        }
    }
    #endregion
    private B06Transition[] transitions = { B06CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B06Base> controller) {
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
