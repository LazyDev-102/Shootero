

using Class_FSM;

public class E01CanAppearTransition : E01Transition
{
    #region Singleton
    public E01CanAppearTransition()
    {

    }
    private static E01CanAppearTransition instance = null;
    public static E01CanAppearTransition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new E01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E01Base> controller)
    {
        bool isTransition = controller.ObjectBase.E01Move.CanMoveAppear();
        if(isTransition)
        {
            controller.TransitionToState(E01MoveState.Instance, this);
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
