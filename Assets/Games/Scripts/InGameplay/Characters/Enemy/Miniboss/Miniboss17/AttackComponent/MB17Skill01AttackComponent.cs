using DG.Tweening;
using Helper;
using UnityEngine;

public class MB17Skill01AttackComponent : MinibossAttackComponent<MB17Attack> {
    [SerializeField] private MB17Base mb17Base;
    [SerializeField] private BoomFrontBullet bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private StatModifier delayAttack;
    [SerializeField] private StatModifier warningTime;
    [SerializeField] private StatModifier attackSpeed;
    [SerializeField] private StatModifier acceleration;
    [SerializeField] private StatModifier boomRadius;
    [SerializeField] private int numberBullet;
    [SerializeField] private Area rangeSpawnBullet;

    private Countdowner delayCD = new Countdowner();
    private Countdowner delayEndAttack = new Countdowner();
    private float deltaTime;
    private bool canAttack;
    private ShipBase ship;

    public override void StartAttack() {
    }
    public override void Attacking() {
        delayCD.StartCountdown(delayAttack.Value);
        delayEndAttack.StartCountdown(warningTime.Value);
        deltaTime = Time.deltaTime;
        canAttack = true;
        ship = GameManager.Instance.GameLoader.Ship;
    }

    public override void Updating() {
        if (!canAttack)
            return;
        if (delayCD.IsCountdowning()) {
            delayCD.Countdowning(deltaTime);
            mb17Base.LookTarget();
            return;
        }
        if (canAttack) {
            canAttack = false;
            for (int i = 0; i < numberBullet; i++) {
                var pos = BorderHelper.GetWorldPointInsideArea(rangeSpawnBullet);
                var bClone = GameManager.Instance.GameLoader.SpawnBullet(bullet, firePoint.position);
                bClone.SetHitInfor(mb17Base.MB17Stat.Atk.Value, null, mb17Base);
                bClone.SetMoveComplete(bClone.WarningEffect, warningTime.Value)
                      .SetTarget(pos)
                      .SetBoomRadius(boomRadius.Value)
                      .Shoot((pos - (Vector2)transform.position).normalized, attackSpeed.Value, acceleration.Value);
            }

            DOVirtual.DelayedCall(warningTime.Value, EndAttack);
        }
    }
    public override void EndAttack() {
        canAttack = false;
        base.EndAttack();
    }
}
