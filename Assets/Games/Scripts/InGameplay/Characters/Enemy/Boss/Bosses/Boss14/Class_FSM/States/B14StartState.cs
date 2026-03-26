using Class_FSM;
using UnityEngine;

public class B14StartState : B14State {
    #region Singleton
    public B14StartState() {

    }
    private static B14StartState instance = null;
    public static B14StartState Instance {
        get {
            if(instance == null) {
                instance = new B14StartState();
            }
            return instance;
        }
    }
    #endregion
    private B14Transition[] transitions = { B14CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B14Base> controller) {
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
