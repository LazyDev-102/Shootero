using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone4AttackComponent : DroneAttackComponent {
    [SerializeField] private AlternateBullet bullet;
    [SerializeField] private Transform leftFirePos;
    [SerializeField] private Transform rightFirePos;
    [SerializeField, Range(0f, 100f)] float speedAttackBase;
    [SerializeField, Range(0f, 100f)] float bulletSpeed;
    [SerializeField] private int numberPreload;

    private bool isLeft;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        base.Initialize();
        droneAtkSpeed.SetBaseValue(speedAttackBase);
        droneAttack.DroneBase.DroneStat.BulletSpeed.SetBaseValue(bulletSpeed);
        attackCountdowner.StartCountdown(FireRate);
    }
    public override void Attack() {
        if (!canAttack)
            return;
        if (attackCountdowner.IsTimeOut()) {
            base.Attack();
            AlternateBullet bulletClone = gameLoader.SpawnBullet(bullet, isLeft ? leftFirePos.position : rightFirePos.position);
            if (bulletClone) {
                bulletClone = ChangingBullet(bulletClone);
                bulletClone.Shoot(droneAttack.DroneBase.DroneStat.BulletSpeed.Value, Vector2.up);
            }
            isLeft = !isLeft;
            attackCountdowner.StartCountdown(FireRate);
        }
        else {
            attackCountdowner.Countdowning(Time.deltaTime);
        }
    }

    public override void Updating() {
        base.Updating();
        Attack();
    }
}
