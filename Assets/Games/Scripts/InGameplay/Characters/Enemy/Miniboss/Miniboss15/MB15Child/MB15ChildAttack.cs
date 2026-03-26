using DG.Tweening;
using Helper;
using UnityEngine;

public class MB15ChildAttack : MinibossAttack {

    private MB15ChildBase mb15ChildBase;

    public MB15ChildBase MB15ChildBase {
        get {
            if (mb15ChildBase == null) {
                mb15ChildBase = MinibossBase as MB15ChildBase;
            }
            return mb15ChildBase;
        }
    }


    [SerializeField] private float delayShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float delayAttack;
    [SerializeField] private Laser[] guns;
    [SerializeField] private ParticleSystem[] gunParticles;
    [SerializeField] private DOTweenAnimation rotateAnim1;
    [SerializeField] private DOTweenAnimation rotateAnim2;

    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();
    private float shotDuration;
    private Tweener rotateTweener;
    private bool canAttack;
    private bool isShooting;


    public override void Destroy() {
        base.Destroy();
        if (rotateTweener != null) {
            rotateTweener.Kill();
        }
    }
    public void SetShotDuration(float duration) {
        shotDuration = duration;
    }
    public override void Initialize() {
        base.Initialize();
        var ran = Random.Range(1f, 2f);
        rotateAnim1.duration = ran;
        rotateAnim2.duration = ran;
        durationCountdowner.StartCountdown(0.5f);
        delayCountdowner.StartCountdown(Random.Range(0f, 2f));
        deltaShotCountdowner.StartCountdown(deltaShot);
    }
    public void Fight() {
        //rotateTweener = transform.DORotate(Vector3.zero, delayShot, RotateMode.FastBeyond360).OnComplete(StartShotGun);
        StartShotGun();
    }
    public override void Updating() {
        if (canAttack) {
            BeamingLaser();
        }
    }

    private void StartShotGun() {
        foreach (var gun in guns) {
            gun.StartBeam();
            gun.gameObject.SetActive(true);
        }

        foreach (var par in gunParticles) {
            par.Play();
        }
        canAttack = true;
        isShooting = true;
    }

    public void BeamingLaser() {
        if (delayCountdowner.IsTimeOut()) {
            if (isShooting) {
                durationCountdowner.Countdowning(Time.deltaTime);
                deltaShotCountdowner.Countdowning(Time.deltaTime);
                foreach (var gun in guns) {
                    if (deltaShotCountdowner.IsTimeOut()) {
                        gun.SetInfor(MB15ChildBase.MB15ChildStat.Atk.Value, null);
                        gun.Beaming(true);
                        deltaShotCountdowner.StartCountdown(deltaShot);
                    }
                    else {
                        gun.Beaming(false);
                    }
                }
                if (durationCountdowner.IsTimeOut()) {
                    EndBeamLaser();
                }
            }
        }
        else {
            delayCountdowner.Countdowning(Time.deltaTime);
            MB15ChildBase.LookTarget();
            if (delayCountdowner.IsTimeOut()) {
                Fight();
            }
        }
    }

    private void EndBeamLaser() {
        isShooting = false;
        foreach (var gun in guns) {
            gun.gameObject.SetActive(false);
            gun.EndBeam();
        }
        foreach (var par in gunParticles) {
            par.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        MB15ChildBase.MB15ChildMove.StartMoveAfterAttack();
        durationCountdowner.StartCountdown(shotDuration);
        delayCountdowner.StartCountdown(Random.Range(delayAttack, delayAttack + 2f));
    }
}
