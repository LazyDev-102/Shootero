

using System;
using UnityEngine;

public class BallBullet : BulletBase {
    private Action<BallBullet> onBallDestroy;

    public void AddOnBallDestroy(Action<BallBullet> onBallDestroy) {
        this.onBallDestroy += onBallDestroy;
    }

    public void RemoveOnBallDestroy(Action<BallBullet> onBallDestroy) {
        this.onBallDestroy -= onBallDestroy;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (IsBlockHit()) {
            return;
        }
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                Hit(collision);
                return;
            }
        }
    }

    public void RemoveAllOnBallDestroy() {
        onBallDestroy = null;
    }

    protected override void RemoveMe() {
        RemoveAllOnBallDestroy();
        base.RemoveMe();
    }

    public override void DestroyWithEffect() {
        onBallDestroy?.Invoke(this);
        base.DestroyWithEffect();
    }

    protected override void Destroy() {
        onBallDestroy?.Invoke(this);
        base.Destroy();
    }

    public void SelfDestroy() {
        RemoveMe();
    }

}
