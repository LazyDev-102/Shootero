

using Class_FSM;

public class E01CanDieTransition : E01Transition
{
    #region Singleton
    public E01CanDieTransition()
    {

    }
    private static E01CanDieTransition instance = null;
    public static E01CanDieTransition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new E01CanDieTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<E01Base> controller)
    {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition)
        {
            controller.TransitionToState(E01DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E01Base> controller)
    {
    }

    public override void DoBeforeTransitionActions(StateController<E01Base> controller)
    {
    }

    public override void DoWhileTransitionActions(StateController<E01Base> controller)
    {
    }
}
