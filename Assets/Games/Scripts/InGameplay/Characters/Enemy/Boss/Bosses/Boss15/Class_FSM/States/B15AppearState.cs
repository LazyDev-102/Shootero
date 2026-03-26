

using Class_FSM;
using UnityEngine;

public class B15AppearState : B15State {
    #region Singleton
    public B15AppearState() {

    }
    private static B15AppearState instance = null;
    public static B15AppearState Instance {
        get {
            if (instance == null) {
                instance = new B15AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B15Transition[] transitions = { B15AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B15Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B15Base> controller) {
        controller.ObjectBase.B15Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
