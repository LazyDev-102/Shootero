
using UnityEngine;


namespace FSM {
    [System.Serializable]
    public class Transition {
        [SerializeField] private string nameTransition;
        [SerializeField] private Decision decision;
        [SerializeField] private State trueState;
        [SerializeField] private State falseState;
        [SerializeField] private Action[] beforeTransitionActions;
        [SerializeField] private Action[] whileTransitionActions;
        [SerializeField] private Action[] afterTransitionActions;
        public string NameTransition { get => nameTransition; }
        public Decision Decision { get => decision; }
        public State TrueState { get => trueState; }
        public State FalseState { get => falseState; }

        public virtual void DoBeforeTransitionActions(StateController controller) {
            beforeTransitionActions.DoAllAction(controller);
        }

        public virtual void DoWhileTransitionActions(StateController controller) {
            whileTransitionActions.DoAllAction(controller);
        }

        public virtual void DoAfterTransitionActions(StateController controller) {
            afterTransitionActions.DoAllAction(controller);
        }
    }
}