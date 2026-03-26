using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B11Attack : BossAttack {
    private B11Base b11Base;
    private List<B11State> attackStates = new List<B11State>() { B11Attack1State.Instance, B11MoveToAttack2State.Instance, B11Attack3State.Instance };

    public B11Base B11Base {
        get {
            if (b11Base == null) {
                b11Base = BossBase as B11Base;
            }
            return b11Base;
        }
    }

    public B11State ChooseState() {
        return attackStates[Random.Range(0, 3)];
    }
    public void ChooseAttack(int index) {
        SetCurrentAttack(skillAttacks[index]);
    }
}
