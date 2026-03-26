using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B09Move : BossMove {
    private Vector2 origin;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }

    public virtual Vector2 GetPointMoveB09(Vector2 point) {
        return GetRandomInArea(point);
    }

}