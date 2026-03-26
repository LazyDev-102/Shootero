using Class_FSM;
using UnityEngine;

public class MB15ParentStateController : StateController<MB15ParentBase> {
    private MB15ParentTransition[] transitions = { MB15ParentIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB15ParentBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB15ParentStartState.Instance);
        MB15ParentStartState.Instance.StartState(this);
    }
}
