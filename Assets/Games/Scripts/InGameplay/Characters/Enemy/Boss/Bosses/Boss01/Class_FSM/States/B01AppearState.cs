

using Class_FSM;
using UnityEngine;

public class B01AppearState : B01State {
    #region Singleton
    public B01AppearState() {

    }
    private static B01AppearState instance = null;
    public static B01AppearState Instance {
        get {
            if (instance == null) {
                instance = new B01AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B01Transition[] transitions = { B01AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B01Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B01Base> controller) {
        controller.ObjectBase.B01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
