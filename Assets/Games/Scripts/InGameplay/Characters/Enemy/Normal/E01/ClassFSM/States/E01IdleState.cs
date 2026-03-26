
using Class_FSM;

public class E01IdleState : E01State {
    #region Singleton
    public E01IdleState() {

    }
    private static E01IdleState instance = null;
    public static E01IdleState Instance {
        get {
            if (instance == null) {
                instance = new E01IdleState();
            }
            return instance;
        }
    }
    #endregion
    private Transition<E01Base>[] transitions = { E01CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E01Base> controller) {
    }

    protected override void DoStartActions(StateController<E01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E01Base> controller) {
        if (Helper.BorderHelper.IsOutBound(controller.transform.position)) {
            controller.ObjectBase.Die();
        }
    }

    protected override Transition<E01Base>[] GetTransitions() {
        return transitions;
    }
}
