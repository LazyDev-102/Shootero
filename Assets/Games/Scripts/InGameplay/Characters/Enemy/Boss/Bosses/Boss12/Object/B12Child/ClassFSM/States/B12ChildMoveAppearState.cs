

using Class_FSM;

public class B12ChildMoveAppearState : B12ChildState {
    #region Singleton
    public B12ChildMoveAppearState() {

    }
    private static B12ChildMoveAppearState instance = null;
    public static B12ChildMoveAppearState Instance {
        get {
            if (instance == null) {
                instance = new B12ChildMoveAppearState();
            }
            return instance;
        }
    }
    #endregion

    private B12ChildTransition[] transitions = { B12ChildHasCompleteAppearTranstion.Instance, B12ChildHasCompleteKnockTransition.Instance };
    protected override void DoEndActions(StateController<B12ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.B12ChildMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B12ChildBase> controller) {
        //controller.ObjectBase.B12ChildMove.MoveDirect();
        //controller.ObjectBase.B12ChildMove.CheckingPositionAppearPoint();
    }

    protected override Transition<B12ChildBase>[] GetTransitions() {
        return transitions;
    }
}
