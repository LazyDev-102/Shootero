

using Class_FSM;

public class E06AimState : E06State {

    #region Singleton
    public E06AimState() {

    }
    private static E06AimState instance = null;
    public static E06AimState Instance {
        get {
            if (instance == null) {
                instance = new E06AimState();
            }
            return instance;
        }
    }
    #endregion
    private E06Transition[] transitions = { E06HasMoveEndTransition.Instance };

    protected override void DoEndActions(StateController<E06Base> controller) {
    }

    protected override void DoStartActions(StateController<E06Base> controller) {
        controller.ObjectBase.E06Attack.StartAimTarget();
        controller.ObjectBase.E06Move.StartMoveIdle();
    }

    protected override void DoUpdateActions(StateController<E06Base> controller) {
        controller.ObjectBase.E06Attack.AimTarget();
    }

    protected override Transition<E06Base>[] GetTransitions() {
        return transitions;
    }
}
