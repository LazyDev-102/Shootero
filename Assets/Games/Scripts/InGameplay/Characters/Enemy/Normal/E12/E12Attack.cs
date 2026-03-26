
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12Attack : EnemyAttack {
    private E12Base e12Base;
    public E12Base E12Base {
        get {
            if (e12Base == null) {
                e12Base = EnemyBase as E12Base;
            }
            return e12Base;
        }
    }

    public override bool CanAttack() {
        return false;
    }

    protected override void Attacking() {

    }
}
