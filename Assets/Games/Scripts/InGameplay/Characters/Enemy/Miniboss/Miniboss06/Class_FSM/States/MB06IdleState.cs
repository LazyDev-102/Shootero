using Class_FSM;
using UnityEngine;

public class MB06IdleState : MB06State {
    #region Singleton
    public MB06IdleState() {

    }
    private static MB06IdleState instance = null;
    public static MB06IdleState Instance {
        get {
            if (instance == null) {
                instance = new MB06IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MB06Transition[] transitions = { MB06CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB06Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<MB06Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MB06Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<MB06Base>[] GetTransitions() {
        return transitions;
    }
}
