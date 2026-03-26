

using Helper;
using UnityEngine;

public abstract class ShotHomingShipPattern : ShotShipPattern<ShotHomingAttackComponent> {
    [SerializeField] private LayerMask maskCheck;
    [SerializeField] private float findTargetOffset = 2f;
    [SerializeField] private float findTargetRadius = 7.2f;
    [SerializeField] private float findTagetDistance = 12.8f;

    protected Transform GetTargetMid(Vector3 direction) {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, findTargetRadius, direction, findTagetDistance, maskCheck);
        if (raycastHit2D) {
            Vector2 point = raycastHit2D.point;
            if (!BorderHelper.IsOutBound(point)) {
                if (raycastHit2D.collider != null) {
                    return raycastHit2D.collider.transform;
                }
            }
        }
        return null;
    }

    protected Transform GetTargetLeft(Vector3 direction) {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + Vector3.left * findTargetOffset, findTargetRadius, direction, findTagetDistance, maskCheck);
        if (raycastHit2D) {
            Vector2 point = raycastHit2D.point;
            if (!BorderHelper.IsOutBound(point)) {
                if (raycastHit2D.collider != null) {
                    return raycastHit2D.collider.transform;
                }
            }
        }
        return null;
    }

    protected Transform GetTargetRight(Vector3 direction) {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + Vector3.right * findTargetOffset, findTargetRadius, direction, findTagetDistance, maskCheck);
        if (raycastHit2D) {
            Vector2 point = raycastHit2D.point;
            if (!BorderHelper.IsOutBound(point)) {
                if (raycastHit2D.collider != null) {
                    return raycastHit2D.collider.transform;
                }
            }
        }
        return null;
    }
}
