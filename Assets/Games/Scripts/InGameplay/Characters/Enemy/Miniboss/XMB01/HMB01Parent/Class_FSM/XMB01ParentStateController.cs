using Class_FSM;
using UnityEngine;

public class XMB01ParentStateController : StateController<XMB01ParentBase> {
    private XMB01ParentTransition[] transitions = { XMB01ParentIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<XMB01ParentBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(XMB01ParentStartState.Instance);
        XMB01ParentStartState.Instance.StartState(this);
    }
}
