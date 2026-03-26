using UnityEngine;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;

public class MB14SpecialAttackComponent : MinibossAttackComponent<MB14Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform shield;
    [SerializeField] private ParticleSystem trail3;
    [SerializeField] private BasicLaser laserCenter;
    [SerializeField] private int laserLeftLength = 3;
    [SerializeField] private int laserCenterLength = 5;
    [SerializeField] private int laserRightLength = 3;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int attackCount;
    [SerializeField] private float aimTime;
    [SerializeField, Tooltip("Effect tụ lại")] private ParticleSystem effectCondense;
    [SerializeField, Tooltip("Effect tỏa ra sau khi Aim")] private ParticleSystem burstEffect;

    private bool activeAim;
    private bool isMoving;
    private int numberAttack = 0;

    private Countdowner deltaShotCountdowner = new Countdowner();

    public override void StartAttack() {
        activeAim = false;
        isMoving = false;
        numberAttack = 0;
        if (gameObject.activeInHierarchy)
            StartCoroutine(IDelayAttack());
        minibossAttack.MB14Base.MB14Move.StopMoveIdle();
    }
    public override void Updating() {
        if (activeAim) {
            minibossAttack.MB14Base.LookTarget();
        }
        if (isMoving) {
            minibossAttack.MB14Base.MB14Move.MoveFront();
            //minibossAttack.transform.position += minibossAttack.transform.up * moveSpeed * Time.deltaTime;
            if (!trail3.isPlaying && trail3 != null) {
                if (trail3 != null)
                    trail3.Play();
                if (burstEffect != null)
                    burstEffect.Play();
                StartBeamLaser();
            }
            BeamingLaser();
            if (minibossAttack.MB14Base.MB14Move.HasOutBorder()) {
                isMoving = false;
                numberAttack++;
                var ranVector2 = new Vector2(0.5f, 1.1f);
                minibossAttack.transform.position = minibossAttack.MB14Base.MB14Move.GetPointMoveMB14(ranVector2);
                ChangeZ(minibossAttack.transform);
                if (numberAttack < attackCount) {
                    isMoving = false;
                    activeAim = true;
                    DOVirtual.DelayedCall(0.1f, () => { activeAim = false; isMoving = true; });
                }
                else {
                    if (trail3 != null)
                        trail3.Stop();
                    var posDefault = new Vector2(0.5f, 0.8f);
                    minibossAttack.transform.DOMove(minibossAttack.MB14Base.MB14Move.GetPointMoveMB14(posDefault), 2f).OnComplete(() => EndAttack());
                }
            }
        }
    }

    public override void Attacking() {
    }

    public override void EndAttack() {
        base.EndAttack();
        isMoving = false;
        activeAim = false;
        minibossAttack.MB14Base.MB14Move.CanKnockBack = true;
        minibossAttack.MB14Base.MB14Move.RestartMoveIdle();
    }

    public override void StopAttack() {
        base.StopAttack();
        isMoving = false;
        activeAim = false;
        minibossAttack.MB14Base.MB14Move.CanKnockBack = true;
        minibossAttack.MB14Base.MB14Move.RestartMoveIdle();
    }

    private IEnumerator IDelayAttack() {
        activeAim = true;
        if (effectCondense != null)
            effectCondense.Play();
        yield return Yielder.Wait(aimTime);
        if (effectCondense != null)
            effectCondense.Stop();
        activeAim = false;
        isMoving = true;
        minibossAttack.MB14Base.MB14Move.SetTargetMoveAttack(minibossAttack.Target.position, moveSpeed);
    }

    private void ChangeZ(Transform trans) {
        var temp = trans.localEulerAngles;
        temp.z = 180;
        trans.localEulerAngles = temp;
    }

    private void StartBeamLaser() {

        laserCenter.StartBeam();

        laserCenter.SetMaxLength(laserCenterLength);

        laserCenter.gameObject.SetActive(true);
    }
    private void EndBeamLaser() {

        laserCenter.EndBeam();

        laserCenter.gameObject.SetActive(false);
    }
    public void BeamingLaser() {
        if (isMoving) {
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            if (deltaShotCountdowner.IsTimeOut()) {
                laserCenter.SetInfor((int)(minibossAttack.CharacterBase.CharacterStat.Atk.Value * damagePercent), null);
                laserCenter.Beaming(true);
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                laserCenter.Beaming(false);
            }
        }
        else {
            EndBeamLaser();
        }
    }
}
