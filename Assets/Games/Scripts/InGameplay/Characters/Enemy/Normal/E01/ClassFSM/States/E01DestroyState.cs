
using Class_FSM;

public class E01DestroyState : E01State
{
    #region Singleton
    public E01DestroyState()
    {

    }
    private static E01DestroyState instance = null;
    public static E01DestroyState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new E01DestroyState();
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
        controller.ObjectBase.SelfDestruction();
    }

    protected override void DoUpdateActions(StateController<E01Base> controller)
    {
    }

    protected override Transition<E01Base>[] GetTransitions()
    {
        return null;
    }
}
