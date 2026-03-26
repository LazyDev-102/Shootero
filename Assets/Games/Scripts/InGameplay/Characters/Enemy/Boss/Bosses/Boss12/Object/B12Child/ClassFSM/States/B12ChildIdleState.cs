

using Class_FSM;

public class B12ChildIdleState : B12ChildState{
    #region Singleton
    public B12ChildIdleState() {

    }
    private static B12ChildIdleState instance = null;
    public static B12ChildIdleState Instance {
        get {
            if(instance == null) {
                instance = new B12ChildIdleState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<B12ChildBase>[] transitons = { B12ChildCanMoveAppearTransition.Instance };

    protected override Transition<B12ChildBase>[] GetTransitions() {
        return transitons;
    }

    protected override void DoStartActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B12ChildBase> controller) {
    }

    protected override void DoEndActions(StateController<B12ChildBase> controller) {
    }


}
