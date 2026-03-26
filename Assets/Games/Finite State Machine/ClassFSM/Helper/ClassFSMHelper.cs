


namespace Class_FSM {
    public static class ClassFSMHelper {
        public static void CheckTransition<T>(this Transition<T>[] transitions, StateController<T> controller) where T : ObjectBase {
            if(transitions != null & controller != null) {
                bool isTransition = false;
                foreach(var transition in transitions) {
                    isTransition = transition.CheckTransition(controller);
                    if(isTransition) {
                        return;
                    }
                }
            }
        }
    }
}
