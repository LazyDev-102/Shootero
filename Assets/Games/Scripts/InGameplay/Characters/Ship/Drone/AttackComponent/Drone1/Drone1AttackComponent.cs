using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone1AttackComponent : DroneAttackComponent {
    [SerializeField] private FrontBullet bullet;
    [SerializeField, Range(0f, 100f)] float baseAttackSpeed;
    [SerializeField, Range(0f, 100f)] float speedBullet;
    [SerializeField] private int numberPreload;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        base.Initialize();
        droneAtkSpeed.SetBaseValue(baseAttackSpeed);
        attackCountdowner.StartCountdown(FireRate);
        droneAttack.DroneBase.DroneStat.BulletSpeed.SetBaseValue(speedBullet);
    }
    public override void Attack() {
        if (!canAttack)
            return;
        if (attackCountdowner.IsTimeOut()) {
            base.Attack();
            FrontBullet bulletClone = gameLoader.SpawnBullet(bullet, firePoint.position);
            if (bulletClone) {
                bulletClone = ChangingBullet(bulletClone);
                bulletClone.Shoot(droneAttack.DroneBase.DroneStat.BulletSpeed.Value, Vector2.up);
            }
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
