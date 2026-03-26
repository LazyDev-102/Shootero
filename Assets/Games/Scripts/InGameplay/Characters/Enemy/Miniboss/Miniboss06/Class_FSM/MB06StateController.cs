using Class_FSM;
using UnityEngine;

public class MB06StateController : StateController<MB06Base> {
    private MB06Transition[] transitions = { MB06IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB06Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB06StartState.Instance);
        MB06StartState.Instance.StartState(this);
    }
}
