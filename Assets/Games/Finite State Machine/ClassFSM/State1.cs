


namespace Class_FSM {
    public class State1 : State<CharacterBase> {
        private State1() {

        }
        private static State1 instance = null;
        public static State1 Instance {
            get {
                if(instance == null) {
                    instance = new State1();
                }
                return instance;
            }
        }

        protected override void DoEndActions(StateController<CharacterBase> controller) {
            throw new System.NotImplementedException();
        }

        protected override void DoStartActions(StateController<CharacterBase> controller) {
            throw new System.NotImplementedException();
        }

        protected override void DoUpdateActions(StateController<CharacterBase> controller) {
            throw new System.NotImplementedException();
        }

        protected override Transition<CharacterBase>[] GetTransitions() {
            throw new System.NotImplementedException();
        }
    }
}
