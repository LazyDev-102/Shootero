

using Class_FSM;

public class E08AimState : E08State {
    #region Singleton
    public E08AimState() {

    }
    private static E08AimState instance = null;
    public static E08AimState Instance {
        get {
            if(instance == null) {
                instance = new E08AimState();
            }
            return instance;
        }
    }
    #endregion
    private E08Transition[] transtions = { E08CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<E08Base> controller) {
    }

    protected override void DoStartActions(StateController<E08Base> controller) {
        controller.ObjectBase.E08Attack.StartAimTarget();
    }

    protected override void DoUpdateActions(StateController<E08Base> controller) {
        controller.ObjectBase.E08Attack.AimTarget();
    }

    protected override Transition<E08Base>[] GetTransitions() {
        return transtions;
    }
}
