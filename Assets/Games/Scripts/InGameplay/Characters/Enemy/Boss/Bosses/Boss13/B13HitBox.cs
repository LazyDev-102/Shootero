
using UnityEngine;

public class B13HitBox : BossHitbox {
    [SerializeField] private Collider2D otherCollider;

    public Collider2D OtherCollider { get => otherCollider; }

    protected override void SetDataAfterFreeze() {
        base.SetDataAfterFreeze();
        otherCollider.enabled = true;
    }
    protected override void SetDataBeforeFreeze(Collider2D collider) {
        base.SetDataBeforeFreeze(collider);
        if (collider.tag.Equals(GameTag.Player)) {
            otherCollider.enabled = false;
        }
    }

    public void ActiveCollider(bool status) {
        otherCollider.enabled = status;
        myCollider.enabled = status;
    }
}
