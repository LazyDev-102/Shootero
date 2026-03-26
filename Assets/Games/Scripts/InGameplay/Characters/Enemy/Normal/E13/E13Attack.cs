
using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E13Attack : EnemyAttack {
    private E13Base e13Base;
    public E13Base E13Base {
        get {
            if (e13Base == null) {
                e13Base = EnemyBase as E13Base;
            }
            return e13Base;
        }
    }

    #region Attack
    [SerializeField] private ParticleSystem effectFire;
    [SerializeField] private Laser laserBullet;
    [SerializeField] private int laserLength = 3;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float delayAttack;
    [SerializeField] private float duration;
    [SerializeField] private float laserSize;

    Countdowner delayCD = new Countdowner();
    private Countdowner durantionCD = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    public override bool CanAttack() {
        return !isAttacking;
    }

    protected override void Attacking() {
        isAttacking = true;
        delayCD.StartCountdown(delayAttack);
        deltaShotCountdowner.StartCountdown(deltaShot);
        durantionCD.StartCountdown(duration);
    }
    public override void Updating() {
        if (delayCD.IsCountdowning()) {
            E13Base.LookTarget();
            delayCD.Countdowning(Time.deltaTime);
        }
        else {
            if (!effectFire.isPlaying && effectFire != null) {
                if (effectFire != null)
                    effectFire.Play();
                StartBeamLaser();
            }
            BeamingLaser();
        }
    }

    private void StartBeamLaser() {
        laserBullet.StartBeam();
        laserBullet.SetMaxLength(laserLength);
        laserBullet.gameObject.SetActive(true);
    }
    private void EndBeamLaser() {
        laserBullet.EndBeam();
        laserBullet.gameObject.SetActive(false);
    }
    public void BeamingLaser() {
        if (durantionCD.IsCountdowning()) {
            durantionCD.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            if (deltaShotCountdowner.IsTimeOut()) {
                laserBullet.SetInfor((int)(E13Base.E13Stat.Atk.Value * damagePercent), null);
                laserBullet.SetRadiusSize(laserSize * E13Base.E13Stat.Size.Value);
                laserBullet.Beaming(true);
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                laserBullet.Beaming(false);
            }
        }
        else {
            EndBeamLaser();
            if (effectFire != null)
                effectFire.Stop();
            EndAttack();
        }
    }
    public override void EndAttack() {
        isAttacking = false;
        base.EndAttack();
    }
    #endregion
}
