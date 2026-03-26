
using UnityEngine;

public class B09RefectorMove : BossMove {
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
    }

    public virtual Vector2 GetPointMoveB09(Vector2 point) {
        return GetRandomInArea(point);
    }

}