

using Class_FSM;

public class ME01MoveState : ME01State {
    #region Singleton
    public ME01MoveState() {

    }
    private static ME01MoveState instance = null;
    public static ME01MoveState Instance {
        get {
            if (instance == null) {
                instance = new ME01MoveState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<ME01Base> controller) {
    }

    protected override void DoStartActions(StateController<ME01Base> controller) {
        controller.ObjectBase.ME01Move.StartMovePath();
        controller.ObjectBase.ME01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<ME01Base> controller) {
        controller.ObjectBase.ME01Attack.RadiatingCircle();

    }

    protected override Transition<ME01Base>[] GetTransitions() {
        return null;
    }
}
