using Class_FSM;
using UnityEngine;

public class MB01ParentStateController : StateController<MB01ParentBase> {
    private MB01ParentTransition[] transitions = { MB01ParentIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB01ParentBase>[] GetTransitionFromAnyState() {
        return transitions;
    }
    protected override void StartStatrState() {
        SetCurrentState(MB01ParentStartState.Instance);
        MB01ParentStartState.Instance.StartState(this);
    }
}
