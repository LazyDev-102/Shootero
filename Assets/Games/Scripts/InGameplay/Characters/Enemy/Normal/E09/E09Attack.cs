

using UnityEngine;

public class E09Attack : EnemyAttack {

    private E09Base e09Base;
    public E09Base E09Base {
        get {
            if (e09Base == null) {
                e09Base = EnemyBase as E09Base;
            }
            return e09Base;
        }
    }

    [SerializeField] private float delayTime;
    [SerializeField] private float aimTime;
    [SerializeField] private float shotDuration;
    [SerializeField] private float deltaShot;
    [SerializeField] private BasicLaser[] lasers;
    [SerializeField] private BasicLaser[] warnings;
    [SerializeField] private float aimRotateSpeed;
    [SerializeField, Range(0, 1)] private float timeOffLaserPercent = 1;
    [SerializeField] private float laserSize;

    private Countdowner aimCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner delayAttackCD = new Countdowner();
    private Countdowner delayFocusCD = new Countdowner();

    private float warningTimeOffPoint;
    private int warningStack = 0;
    private int warningMaxStack = 4;
    private float warningAlpha = 0.5f;

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
        delayAttackCD.StartCountdown(delayTime);
        delayFocusCD.StartCountdown(0.5f);
        warningTimeOffPoint = delayTime / 2;
    }

    public void AimTarget() {
        E09Base.LookTarget();
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut();
    }

    protected override void Attacking() {
        StartBeamLaser();
    }

    public override void EndAttack() {
        EndBeamLaser();
        base.EndAttack();
    }
    private void OnDisable() {
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
            laser.SetRadiusSize(laserSize * E09Base.E09Stat.Size.Value);
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
            if (delayFocusCD.IsTimeOut()) {
                E09Base.E09Move.LookTarget(Target.position, aimRotateSpeed);
            }
            else
                delayFocusCD.Countdowning(Time.deltaTime);
            foreach (var laser in lasers) {
                if (deltaShotCountdowner.IsTimeOut()) {
                    laser.SetInfor(E09Base.E09Stat.Atk.Value, null);
                    laser.Beaming(true);
                    deltaShotCountdowner.StartCountdown(deltaShot);
                }
                else {
                    laser.Beaming(false);
                }
            }
        }
        else {
            delayAttackCD.Countdowning(Time.deltaTime);
            DrawWarning();
            //E09Base.LookTarget();
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
        foreach (var wa in warnings) {
            wa.gameObject.SetActive(false);
            wa.EndBeam();
        }
    }
}
