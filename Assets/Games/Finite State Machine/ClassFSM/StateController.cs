using UnityEngine;
using Gemmob;

namespace Class_FSM {
    public abstract class StateController : MonoBehaviour {

        public virtual void Initialize() {

        }

        public virtual void Updating() {

        }

        public virtual void Destroy() {

        }
    }

    public abstract class StateController<T> : StateController where T : ObjectBase {
        private T objectBase;
        State<T> currentState;
        private float stateTimeElapsed;

        public T ObjectBase {
            get {
                if (objectBase == null) {
                    objectBase = GetComponent<T>();
                    if (objectBase == null) {
                        Logs.LogError("Character is NULL", this);
                    }
                }
                return objectBase;
            }
        }

        public override void Initialize() {
            StartStatrState();
        }

        protected abstract void StartStatrState();

        public sealed override void Updating() {
            // Do Always Actions
            DoAlwaysActions();
            // Transition From Any States
            CheckTransitionFromAnyStates();
            // Update Current State
            currentState?.UpdateState(this);
            stateTimeElapsed += Time.deltaTime;
        }

        protected abstract Transition<T>[] GetTransitionFromAnyState();
        protected abstract void DoAlwaysActions();
        protected void CheckTransitionFromAnyStates() {
            GetTransitionFromAnyState().CheckTransition(this);
        }

        public void TransitionToState(State<T> nextState, Transition<T> transition) {
            if (nextState != null && nextState != currentState && currentState != null) {
                transition.DoBeforeTransitionActions(this);
                currentState.EndState(this);
                transition.DoWhileTransitionActions(this);
                SetCurrentState(nextState);
                currentState.StartState(this);
                transition.DoAfterTransitionActions(this);
            }
        }

        public void SetCurrentState(State<T> currentState) {
            this.currentState = currentState;
        }

        public bool CheckIfCountDownElapsed(float duration) {
            return (stateTimeElapsed >= duration);
        }

        protected virtual void OnExitState() {
            stateTimeElapsed = 0;
        }

        void OnDrawGizmos() {
            if (currentState != null) {
                Gizmos.color = currentState.SceneGizmoColor;
                Gizmos.DrawWireSphere(transform.position, 0.5f);
            }
        }
    }
}
