
using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E14Attack : EnemyAttack {
    #region References
    private E14Base e14Base;
    public E14Base E14Base {
        get {
            if (e14Base == null) {
                e14Base = EnemyBase as E14Base;
            }
            return e14Base;
        }
    }
    #endregion

    #region Attack
    [SerializeField] private BoomFrontBullet bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private StatModifier delayAttack;
    [SerializeField] private StatModifier warningTime;
    [SerializeField] private StatModifier attackSpeed;
    [SerializeField] private StatModifier acceleration;
    [SerializeField] private StatModifier boomRadius;

    private Countdowner delayCD = new Countdowner();
    private Countdowner delayEndAttack = new Countdowner();
    private float deltaTime;
    private bool canAttack;

    protected override void Attacking() {
        delayCD.StartCountdown(delayAttack.Value);
        delayEndAttack.StartCountdown(warningTime.Value);
        deltaTime = Time.deltaTime;
        canAttack = true;
    }

    public override void Updating() {
        if (!canAttack)
            return;
        if (delayCD.IsCountdowning()) {
            delayCD.Countdowning(deltaTime);
            E14Base.LookTarget();
            return;
        }
        if (CanAttack()) {
            canAttack = false;
            var bClone = GameManager.Instance.GameLoader.SpawnBullet(bullet, firePoint.position);
            bClone = ChangingBullet(bClone);
            bClone.SetMoveComplete(bClone.WarningEffect, warningTime.Value)
                  .SetTarget(Target.position)
                  .SetBoomRadius(boomRadius.Value)
                  .Shoot((Target.position - transform.position).normalized, attackSpeed.Value, acceleration.Value);
            DOVirtual.DelayedCall(warningTime.Value, EndAttack);
        }
    }
    public override void EndAttack() {
        canAttack = false;
        base.EndAttack();
    }

    public override bool CanAttack() {
        return canAttack;
    }
    #endregion
}
