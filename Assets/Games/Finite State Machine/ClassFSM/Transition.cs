



namespace Class_FSM {
    public abstract class Transition<T> where T : ObjectBase {
        public abstract bool CheckTransition(StateController<T> controller);
        public abstract void DoBeforeTransitionActions(StateController<T> controller);
        public abstract void DoWhileTransitionActions(StateController<T> controller);
        public abstract void DoAfterTransitionActions(StateController<T> controller);
    }
}