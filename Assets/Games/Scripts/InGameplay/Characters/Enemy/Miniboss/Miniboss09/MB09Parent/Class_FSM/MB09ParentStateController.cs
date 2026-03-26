using Class_FSM;
using UnityEngine;

public class MB09ParentStateController : StateController<MB09ParentBase> {
    private MB09ParentTransition[] transitions = { MB09ParentIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB09ParentBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB09ParentStartState.Instance);
        MB09ParentStartState.Instance.StartState(this);
    }
}
