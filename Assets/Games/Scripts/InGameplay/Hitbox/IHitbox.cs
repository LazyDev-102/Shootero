
using UnityEngine;

public interface IHitbox {
    void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal);
    Transform Transform();
}
