

using Class_FSM;

public class B03StaggerState : B03State {
    #region Singleton
    public B03StaggerState() {

    }
    private static B03StaggerState instance = null;
    public static B03StaggerState Instance {
        get {
            if (instance == null) {
                instance = new B03StaggerState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03EndStagegerTransition.Instance, B03CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
        controller.ObjectBase.RestoreAllShield();
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.StartStagger();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
        controller.ObjectBase.UpdatingStagger();
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
