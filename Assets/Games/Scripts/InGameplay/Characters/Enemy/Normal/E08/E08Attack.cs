

using UnityEngine;

public class E08Attack : EnemyAttack {

    private E08Base e08Base;
    public E08Base E08Base {
        get {
            if (e08Base == null) {
                e08Base = EnemyBase as E08Base;
            }
            return e08Base;
        }
    }

    [SerializeField] private float aimTime;
    [SerializeField] private float shotDuration;
    [SerializeField] private float deltaShot;
    [SerializeField] private float delayTime;
    [SerializeField] private BasicLaser[] lasers;
    [SerializeField] private BasicLaser[] warnings;
    [SerializeField, Range(0, 1)] private float timeOffLaserPercent = 1;
    private float warningTimeOffPoint;
    [SerializeField] private float laserSize;

    private Countdowner aimCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner delayAttackCD = new Countdowner();

    private int warningStack = 0;
    private int warningMaxStack = 4;
    private float warningAlpha = 0.5f;
    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
        delayAttackCD.StartCountdown(delayTime);
        warningTimeOffPoint = delayTime / 2;
    }

    public void AimTarget() {
        E08Base.LookTarget();
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    public override void Initialize() {
        base.Initialize();
        foreach (var laser in lasers) {
            laser.gameObject.SetActive(false);
        }
        foreach (var item in warnings) {
            item.gameObject.SetActive(false);
        }
    }

    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut();
    }

    protected override void Attacking() {
        StartBeamLaser();
    }

    public override void EndAttack() {
        base.EndAttack();
        EndBeamLaser();
    }

    public bool IsEndLaser() {
        return durationCountdowner.IsTimeOut();
    }

    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(shotDuration);
        deltaShotCountdowner.StartCountdown(deltaShot);
        foreach (var laser in lasers) {
            laser.StartBeam();
            laser.gameObject.SetActive(true);
            laser.SetRadiusSize(laserSize * E08Base.E08Stat.Size.Value);
        }
        foreach (var item in warnings) {
            item.StartBeam();
            item.gameObject.SetActive(true);
            item.SetAlphaLaser(1);
            item.SetRadiusSize(laserSize * E08Base.E08Stat.Size.Value / 2);
        }
    }

    public void BeamingLaser() {
        if (delayAttackCD.IsTimeOut()) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            float timeOffPoint = shotDuration * (1 - timeOffLaserPercent);
            float timeOffElapsed = durationCountdowner.Countdown;
            if (timeOffElapsed < timeOffPoint) {
                float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
                foreach (var laser in lasers) {
                    laser.SetPercentSize(percentSize);
                }
            }
            if (deltaShotCountdowner.IsTimeOut()) {
                foreach (var laser in lasers) {
                    laser.SetInfor(E08Base.E08Stat.Atk.Value, null);
                    laser.Beaming(true);
                }
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                foreach (var laser in lasers) {
                    laser.Beaming(false);
                }
            }
        }
        else {
            delayAttackCD.Countdowning(Time.deltaTime);
            DrawWarning();
            E08Base.LookTarget();
        }
    }

    private void DrawWarning() {
        if (delayAttackCD.Countdown < warningTimeOffPoint) {
            if (warningStack % warningMaxStack == 0) {
                foreach (var waring in warnings) {
                    waring.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
                }
            }
            warningStack++;
        }
        foreach (var waring in warnings) {
            if (delayAttackCD.Countdown < warningTimeOffPoint) {
                float percentSize = warningTimeOffPoint == 0 ? 1 : delayAttackCD.Countdown / warningTimeOffPoint;
                waring.SetPercentSize(percentSize);
            }
            waring.gameObject.SetActive(true);
            waring.Beaming(false);
        }
    }
    private void EndBeamLaser() {
        foreach (var laser in lasers) {
            laser.gameObject.SetActive(false);
            laser.EndBeam();
        }
    }
}
