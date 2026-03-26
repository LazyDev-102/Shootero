using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB14CanSpecialTransition : MB14Transition {

    #region Singleton
    public MB14CanSpecialTransition() {

    }
    private static MB14CanSpecialTransition instance = null;
    public static MB14CanSpecialTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14CanSpecialTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = controller.ObjectBase.IsSpecialState;
        if (isTransition) {
            controller.TransitionToState(MB14SpecialState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }
}
