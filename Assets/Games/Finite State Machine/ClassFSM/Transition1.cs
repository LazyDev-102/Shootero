using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Class_FSM {
    public class Transition1 : Transition<CharacterBase> {
        public static Transition1 Instance;
        public override bool CheckTransition(StateController<CharacterBase> controller) {
            return true;
        }

        public override void DoAfterTransitionActions(StateController<CharacterBase> controller) {
        }

        public override void DoBeforeTransitionActions(StateController<CharacterBase> controller) {
        }

        public override void DoWhileTransitionActions(StateController<CharacterBase> controller) {
        }
    }
}
