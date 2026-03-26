

using Class_FSM;
using UnityEngine;

public class B06AppearState : B06State {
    #region Singleton
    public B06AppearState() {

    }
    private static B06AppearState instance = null;
    public static B06AppearState Instance {
        get {
            if (instance == null) {
                instance = new B06AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B06Transition[] transitions = { B06AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B06Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
