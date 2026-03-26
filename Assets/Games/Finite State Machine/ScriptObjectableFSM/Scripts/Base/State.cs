using UnityEngine;
namespace FSM {
    [CreateAssetMenu(menuName = "FSM/State")]
    public class State : ScriptableObject {
        [SerializeField] private string nameState;
        [SerializeField] private Action[] startActions;
        [SerializeField] private Action[] updateActions;
        [SerializeField] private Action[] endActions;
        [SerializeField] private Transition[] transitions;
        [SerializeField] protected Color sceneGizmoColor = Color.grey;
        public Color SceneGizmoColor { get => sceneGizmoColor; private set { } }
        public string NameState { get => nameState; }

        public void StartState(StateController controller) {
            DoStartActions(controller);
        }

        public void UpdateState(StateController controller) {
            DoUpdateActions(controller);
            CheckTransitions(controller);
        }

        public void EndState(StateController controller) {
            DoEndActions(controller);
        }

        protected virtual void DoStartActions(StateController controller) {
            startActions.DoAllAction(controller);
        }

        protected virtual void DoUpdateActions(StateController controller) {
            updateActions.DoAllAction(controller);
        }

        protected virtual void DoEndActions(StateController controller) {
            endActions.DoAllAction(controller);
        }

        protected virtual void CheckTransitions(StateController controller) {
            foreach(Transition transition in transitions) {
                bool decisionSucceeded = transition.Decision.DecideWitElapssed(controller);
                if(decisionSucceeded) {
                    controller.TransitionToState(transition.TrueState, transition);
                }
                else {
                    controller.TransitionToState(transition.FalseState, transition);
                }
            }
        }

    }
}