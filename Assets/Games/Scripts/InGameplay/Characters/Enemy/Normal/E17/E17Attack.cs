using Gemmob;
using System.Collections;
using UnityEngine;

public class E17Attack : EnemyAttack {
    private E17Base e17Base;
    public E17Base E17Base {
        get {
            if (e17Base == null) {
                e17Base = EnemyBase as E17Base;
            }
            return e17Base;
        }
    }

    #region Attack
    [SerializeField] private E17BulletAttack e17Bullet;
    [SerializeField] private float delayAttack;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float duration;
    private int numberShot;
    private bool canAim;
    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }
    private IEnumerator IShotting() {
        canAim = true;
        yield return Yielder.Wait(delayAttack);
        canAim = false;
        e17Bullet.TurnEffect(true);
        SetNumberShot();
        SetBulletInfo();
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            if (gameObject.activeInHierarchy)
                StartCoroutine(e17Bullet.IShotting());
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }
    public override void EndAttack() {
        e17Bullet.TurnEffect(false);
        base.EndAttack();
    }
    public void Aim() {
        if (!canAim)
            return;
        E17Base.E17Move.LookTarget(Target.transform.position);
    }
    private void SetNumberShot() {
        numberShot = (int)(duration / deltaShot);
        if (numberShot < 1)
            numberShot = 1;
    }
    private void SetBulletInfo() {
        int damage = (int)(E17Base.E17Stat.Atk.Value * E17Base.E17Stat.ColliderDamage.Value * damagePercent);
        e17Bullet.SetInfo(damage, deltaShot, E17Base);
    }
    #endregion
}
