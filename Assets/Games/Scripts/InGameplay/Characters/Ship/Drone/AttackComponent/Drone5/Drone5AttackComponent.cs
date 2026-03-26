
using Gemmob;
using UnityEngine;

public class Drone5AttackComponent : DroneAttackComponent {
    [SerializeField] private RotateFrontBullet bullet;
    [SerializeField, Range(0f, 100f)] float baseAttackSpeed;
    [SerializeField, Range(0f, 100f)] float speedBullet;
    [SerializeField, Range(0f, 100f)] float deltaAttack;
    [SerializeField, Range(0f, 100f)] float bulletSize;
    [SerializeField, Range(-360f, 360f)] float rotateSpeed;
    [SerializeField, Range(-360f, 360f)] float rotateAcceler;
    [SerializeField] private ParticleSystem muzzle;
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
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = firePoint.up;
            RotateFrontBullet newBullet = GameManager.Instance.GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.Shoot(directionShot, speedBullet);
                newBullet.SetInfo(deltaAttack);
                newBullet.SetSize(bulletSize);
                newBullet.SetRotateSpeed(rotateSpeed, rotateAcceler);
            }
            attackCountdowner.StartCountdown(FireRate);
        } else {
            attackCountdowner.Countdowning(Time.deltaTime);
        }
        
    }

    public override void Updating() {
        base.Updating();
        Attack();
    }
}
