using Class_FSM;
using UnityEngine;

public class B14ChildAppearState : B14ChildState {

    #region Singleton
    public B14ChildAppearState() {

    }
    private static B14ChildAppearState instance = null;
    public static B14ChildAppearState Instance {
        get {
            if (instance == null) {
                instance = new B14ChildAppearState();
            }
            return instance;
        }
    }
    #endregion

    private B14ChildTransition[] transitions = { B14ChildAppearCompleteTransition.Instance };

    protected override void DoEndActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.B14ChildMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B14ChildBase> controller) {
    }

    protected override Transition<B14ChildBase>[] GetTransitions() {
        return transitions;
    }
}
