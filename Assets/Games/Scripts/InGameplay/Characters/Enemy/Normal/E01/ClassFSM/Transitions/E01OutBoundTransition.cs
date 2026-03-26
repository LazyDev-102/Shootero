
using Class_FSM;
using TMPro;

public class E01OutBoundTransition : E01Transition
{
    #region Singleton
    public E01OutBoundTransition()
    {

    }
    private static E01OutBoundTransition instance = null;
    public static E01OutBoundTransition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new E01OutBoundTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<E01Base> controller)
    {
        bool isTransition = controller.ObjectBase.E01Move.HasOutBorder();
        if(isTransition)
        {
            controller.TransitionToState(E01DestroyState.Instance, this);
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
