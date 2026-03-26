using DG.Tweening;
using UnityEngine;

public class ME02B08Attack : EnemyAttack {
    private ME02B08Base me02Base;
    public ME02B08Base ME02B08Base {
        get {
            if (me02Base == null) {
                me02Base = EnemyBase as ME02B08Base;
            }
            return me02Base;
        }
    }

    [SerializeField] private float delayShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private Laser[] guns;
    [SerializeField] private ParticleSystem[] gunParticles;

    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private float shotDuration;
    private Tweener rotateTweener;
    private bool isShoting;

    public override void Destroy() {
        base.Destroy();
        if (rotateTweener != null) {
            rotateTweener.Kill();
        }
    }

    public override void Initialize() {
        base.Initialize();
        isShoting = false;
    }

    public void SetShotDuration(float duration) {
        shotDuration = duration;
    }

    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        rotateTweener = transform.DORotate(Vector3.zero, delayShot, RotateMode.FastBeyond360).OnComplete(StartShotGun);
    }

    private void StartShotGun() {
        isShoting = true;
        durationCountdowner.StartCountdown(shotDuration);
        deltaShotCountdowner.StartCountdown(deltaShot);
        foreach (var gun in guns) {
            gun.StartBeam();
            gun.gameObject.SetActive(true);
        }

        foreach (var par in gunParticles) {
            par.Play();
        }
    }

    public void BeamingLaser() {
        if (isShoting) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            foreach (var gun in guns) {
                if (deltaShotCountdowner.IsTimeOut()) {
                    gun.SetInfor(ME02B08Base.ME02B08Stat.Atk.Value, null);
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

    private void EndBeamLaser() {
        foreach (var gun in guns) {
            gun.gameObject.SetActive(false);
            gun.EndBeam();
        }
        foreach (var par in gunParticles) {
            par.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        EndAttack();
        ME02B08Base.EndBossAttack();
        ME02B08Base.SelfDestruction();
    }
}
