using Class_FSM;

public class E01DeadState : E01State
{
    #region Singleton
    public E01DeadState()
    {

    }
    private static E01DeadState instance = null;
    public static E01DeadState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new E01DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<E01Base> controller)
    {
    }

    protected override void DoStartActions(StateController<E01Base> controller)
    {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E01Base> controller)
    {
    }

    protected override Transition<E01Base>[] GetTransitions()
    {
        return null;
    }
}
