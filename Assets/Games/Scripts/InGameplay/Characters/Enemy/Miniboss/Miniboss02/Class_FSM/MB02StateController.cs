using Class_FSM;
using UnityEngine;

public class MB02StateController : StateController<MB02Base> {
    private MB02Transition[] transitions = { MB02IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB02Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB02StartState.Instance);
        MB02StartState.Instance.StartState(this);
    }
}
