using Class_FSM;
using UnityEngine;

public class MB16StateController : StateController<MB16Base> {
    private MB16Transition[] transitions = { MB16IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB16Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB16StartState.Instance);
        MB16StartState.Instance.StartState(this);
    }
}
