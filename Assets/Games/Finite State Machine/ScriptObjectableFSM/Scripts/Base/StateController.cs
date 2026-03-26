using System.Collections.Generic;
using UnityEngine;

namespace FSM {
    public class StateController : MonoBehaviour {
        private ObjectBase objectBase;
        [SerializeField] private State startState;
        [SerializeField] private State remainState;
        [SerializeField] private Action[] alwaysUpdates;
        [SerializeField] private Transition[] transitionsFromAnyState;
        private float stateTimeElapsed;
        [SerializeField] private State currentState;

        public ObjectBase Character {
            get {
                if(objectBase == null) {
                    objectBase = GetComponent<CharacterBase>();
                    if(objectBase == null) {
                        FSMHelper.LogError("Character is NULL", this.name);
                    }
                }
                return objectBase;
            }
        }

        public void SetCurrentState(State currentState) {
            this.currentState = currentState;
        }

        void Awake() {
            objectBase = GetComponent<ObjectBase>();
        }

        public virtual void Initialize() {
            SetCurrentState(startState);
            startState.StartState(this);
        }

        public virtual void Destroy() {

        }

        public virtual void Updating() {
            alwaysUpdates.DoAllAction(this);
            // any state to state
            foreach(var transition in transitionsFromAnyState) {
                bool decisionSucceeded = transition.Decision.DecideWitElapssed(this);
                if(decisionSucceeded) {
                    TransitionToState(transition.TrueState, transition);
                    break;
                }
            }
            currentState.UpdateState(this);
            stateTimeElapsed += Time.deltaTime;
        }

        public void TransitionToState(State nextState, Transition transition) {
            if(nextState != remainState && currentState != nextState) {
                FSMHelper.Log(string.Format("Transition: {0} to {1} by {2}", currentState.NameState.AddColorToString(currentState.SceneGizmoColor), nextState.NameState.AddColorToString(nextState.SceneGizmoColor), transition.NameTransition), this.name);
                transition.DoBeforeTransitionActions(this);
                currentState.EndState(this);
                transition.DoWhileTransitionActions(this);
                SetCurrentState(nextState);
                transition.DoAfterTransitionActions(this);
                nextState.StartState(this);
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration) {
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState() {
            stateTimeElapsed = 0;
        }

        void OnDrawGizmos() {
            if(currentState != null) {
                Gizmos.color = currentState.SceneGizmoColor;
                Gizmos.DrawWireSphere(transform.position, 0.5f);
            }
        }
    }
}
