using UnityEngine;
namespace FSM {
    public abstract class Decision : ScriptableObject {
        [SerializeField] protected bool useElapsedTime;
        [SerializeField] protected float elapsedTime;
        public float ElapsedTime { get => elapsedTime; }

        public bool DecideWitElapssed(StateController controller) {
            if (useElapsedTime && !controller.CheckIfCountDownElapsed(elapsedTime)) {
                return false;
            }
            return Decide(controller);
        }
        protected abstract bool Decide(StateController controller);
    }
}