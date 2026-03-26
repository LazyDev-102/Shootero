using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B12Attack : BossAttack {
    private B12Base b12Base;
    private List<B12State> attackStates = new List<B12State>() { B12Attack1State.Instance, B12MoveToAttack2State.Instance, B12Attack3State.Instance };

    public B12Base B12Base {
        get {
            if (b12Base == null) {
                b12Base = BossBase as B12Base;
            }
            return b12Base;
        }
    }

    public B12State ChooseState() {
        return attackStates[Random.Range(0, 3)];
    }
    public void ChooseAttack(int index) {
        SetCurrentAttack(skillAttacks[index]);
    }
}
