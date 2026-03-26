
using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E16Attack : EnemyAttack {
    private E16Base e16Base;
    public E16Base E16Base {
        get {
            if (e16Base == null) {
                e16Base = EnemyBase as E16Base;
            }
            return e16Base;
        }
    }

    #region Attack
    [SerializeField] private Transform firePoint;
    [SerializeField] private SinBullet bullet;
    [SerializeField] private float amplitudeRange;
    [SerializeField] private float cycleRange;
    [SerializeField] private StatModifier delayAttack;
    [SerializeField] private StatModifier numberShot;
    [SerializeField] private StatModifier damagePercent;
    [SerializeField] private StatModifier deltaShot;
    [SerializeField] private StatModifier bulletSpeed;
    private bool attacking;
    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        attacking = false;
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }
    private IEnumerator IShotting() {
        attacking = true;
        yield return Yielder.Wait(delayAttack.Value);
        for (int ishot = 0; ishot < numberShot.Value; ++ishot) {
            SinBullet newBulletRight = GameManager.Instance.GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBulletRight) {
                Vector2 direction = (Target.transform.position - newBulletRight.transform.position).normalized;
                newBulletRight.SetHitInfor((int)(E16Base.E16Stat.Atk.Value * damagePercent.Value), null, EnemyBase);
                newBulletRight.Shoot(bulletSpeed.Value, direction, amplitudeRange, cycleRange);
            }

            SinBullet newBulletLeft = GameManager.Instance.GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBulletLeft) {
                Vector2 direction = (Target.transform.position - newBulletLeft.transform.position).normalized;
                newBulletLeft.SetHitInfor((int)(EnemyBase.EnemyStat.Atk.Value * damagePercent.Value), null, EnemyBase);
                newBulletLeft.Shoot(bulletSpeed.Value, direction, amplitudeRange, cycleRange, false);
            }
            yield return Yielder.Wait(deltaShot.Value);
        }
        EndAttack();
    }
    public override void Updating() {
        if (attacking) {
            AimTarget();
        }
    }
    public override void EndAttack() {
        attacking = false;
        base.EndAttack();
    }
    public void AimTarget() {
        E16Base.LookTarget();
    }
    #endregion
}
