using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B05Attack : BossAttack {
    private B05Base b05Base;
    private List<B05State> attackStates = new List<B05State>() { B05Attack1State.Instance, B05MoveToAttack2State.Instance, B05Attack3State.Instance };

    public B05Base B05Base {
        get {
            if(b05Base == null) {
                b05Base = BossBase as B05Base;
            }
            return b05Base;
        }
    }

    public B05State ChooseState() {
       return attackStates[Random.Range(0, 3)];
    }
}
