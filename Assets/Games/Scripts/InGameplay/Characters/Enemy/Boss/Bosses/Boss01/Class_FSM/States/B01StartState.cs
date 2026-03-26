

using Class_FSM;
using UnityEngine;

public class B01StartState : B01State {
    #region Singleton
    public B01StartState() {

    }
    private static B01StartState instance = null;
    public static B01StartState Instance {
        get {
            if(instance == null) {
                instance = new B01StartState();
            }
            return instance;
        }
    }
    #endregion
    private B01Transition[] transitions = { B01CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B01Base> controller) {
    }

    protected override void DoStartActions(StateController<B01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
