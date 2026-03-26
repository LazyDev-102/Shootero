using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B12HPBulletBase : FrontBullet {
    [SerializeField] private int hp;

    private int currentHP;
    public override void Initalize() {
        base.Initalize();
        currentHP = hp;
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Respawn)) {
            Destroy();
        }
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
    protected override void Hit(Collider2D collision) {
        isHitted = true;
        GetComponent<Collider2D>().enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
        }
        currentHP -= GameManager.Instance.GameLoader.Ship.ShipStat.Atk.Value;
        if (currentHP < 0)
            DestroyWithEffect();
    }

}
