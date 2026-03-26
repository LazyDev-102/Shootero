
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TurretShotXPatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotXPatternAction")]
public class TurretShotXPatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private ShotXPatternData data;

    protected readonly float defaultDistance = 90f;

    public override void Execute(TurretBase target, object user, Action onCompleted) {
        Attack(target, onCompleted);
    }

    public override void Execute(TurretBase target, Action onCompleted) {
        Attack(target, onCompleted);
    }

    public override void RemoveExecute(TurretBase target, object user, Action onCompleted) {
    }
    private void Attack(TurretBase target, Action onCompleted) {
        var gameLoader = GameManager.Instance.GameLoader;
        var ship = gameLoader.Ship;
        var turretAttackComponent = target.TurretAttack.TurretAttackComponent;
        var firePoint = turretAttackComponent.FirePoint;
        ShotXPatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);

        ShootBullet(patternInfo, firePoint.up, firePoint.right, gameLoader, target);
        ShootBullet(patternInfo, firePoint.up, firePoint.right * -1, gameLoader, target);
        ShootBullet(patternInfo, firePoint.up * -1, firePoint.right, gameLoader, target);
        ShootBullet(patternInfo, firePoint.up * -1, firePoint.right * -1, gameLoader, target);
    }
    protected virtual void ShootBullet(ShotXPatternInfo info, Vector2 posA, Vector2 posB, GameLoader gameLoader, TurretBase target) {
        var turretAttackComponent = target.TurretAttack.TurretAttackComponent;
        var speed = turretAttackComponent.TurretAtkSpeed.Value;
        var damage = target.TurretStat.GetFinalDamageWeapon;
        var firePoint = turretAttackComponent.FirePoint;

        for (int ibullet = 0; ibullet < info.NumberBullet; ++ibullet) {
            Vector2 direction = Vector2.Lerp(posA, posB, (info.AngleStart + ibullet * info.AngleDistance) / defaultDistance);
            FrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, firePoint.position);
            if (goLeft) {
                goLeft.SetHitInfor(damage, null, target);
                goLeft.Shoot(speed, direction);
            }
        }
    }
}
