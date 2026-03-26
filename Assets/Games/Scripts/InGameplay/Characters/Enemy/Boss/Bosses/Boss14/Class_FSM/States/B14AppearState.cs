

using Class_FSM;
using UnityEngine;

public class B14AppearState : B14State {
    #region Singleton
    public B14AppearState() {

    }
    private static B14AppearState instance = null;
    public static B14AppearState Instance {
        get {
            if(instance == null) {
                instance = new B14AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B14Transition[] transitions = { B14AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B14Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.MoveDirect();
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
