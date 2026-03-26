using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B06Attack : BossAttack {
    private B06Base b06Base;
    private List<B06State> attackStates = new List<B06State>() { B06Attack1State.Instance, B06Attack2State.Instance, B06Attack3State.Instance };

    public B06Base B06Base {
        get {
            if(b06Base == null) {
                b06Base = BossBase as B06Base;
            }
            return b06Base;
        }
    }

    public B06State ChooseState() {
       return attackStates[Random.Range(0, 3)];
    }
    public void B06ChooseAttack(int index) {
        SetCurrentAttack(skillAttacks[index]);
    }
}
