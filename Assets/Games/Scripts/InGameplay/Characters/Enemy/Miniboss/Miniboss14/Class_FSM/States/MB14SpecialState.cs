using Class_FSM;
using UnityEngine;

public class MB14SpecialState : MB14State {

    #region Singleton
    public MB14SpecialState() {

    }
    private static MB14SpecialState instance = null;
    public static MB14SpecialState Instance {
        get {
            if (instance == null) {
                instance = new MB14SpecialState();
            }
            return instance;
        }
    }
    #endregion

    private MB14Transition[] transitions = { MB14EndSpecialTransition.Instance };
    protected override void DoEndActions(StateController<MB14Base> controller) {
        controller.ObjectBase.MB14Attack.EndSpecialAttack();
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
        controller.ObjectBase.MB14Attack.StopAttack();
        controller.ObjectBase.MB14Attack.StartSpecialAttack();
    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return transitions;
    }
}
