

using Class_FSM;

public class E09AimState : E09State {
    #region Singleton
    public E09AimState() {

    }
    private static E09AimState instance = null;
    public static E09AimState Instance {
        get {
            if(instance == null) {
                instance = new E09AimState();
            }
            return instance;
        }
    }
    #endregion
    private E09Transition[] transtions = { E09CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<E09Base> controller) {
    }

    protected override void DoStartActions(StateController<E09Base> controller) {
        controller.ObjectBase.E09Attack.StartAimTarget();
    }

    protected override void DoUpdateActions(StateController<E09Base> controller) {
        controller.ObjectBase.E09Attack.AimTarget();
    }

    protected override Transition<E09Base>[] GetTransitions() {
        return transtions;
    }
}
