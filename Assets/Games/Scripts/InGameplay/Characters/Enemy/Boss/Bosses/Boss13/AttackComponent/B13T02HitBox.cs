using UnityEngine;

public class B13T02HitBox : MonoBehaviour, IHitbox {
    private B13Base owner;

    public void SetOwner(B13Base owner) {
        this.owner = owner;
    }

    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (owner != null) {
            owner.B13Health.AddHp(-hit.Damage.Value, false);
            TextShowupManager.Instance.ShowHitText(HitType.Normal, $" {hit.Damage.Value}", transform.position);
        }
    }

    public Transform Transform() {
        return transform;
    }
}
