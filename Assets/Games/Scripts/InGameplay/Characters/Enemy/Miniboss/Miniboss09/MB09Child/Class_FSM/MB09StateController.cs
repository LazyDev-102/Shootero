using Class_FSM;
using UnityEngine;

public class MB09StateController : StateController<MB09Base> {
    private MB09Transition[] transitions = { MB09IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB09Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB09StartState.Instance);
        MB09StartState.Instance.StartState(this);
    }
}
