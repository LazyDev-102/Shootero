using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class ME03B08Attack : EnemyAttack {
    private ME03B08Base me03B08Base;
    public ME03B08Base ME03B08Base {
        get {
            if (me03B08Base == null) {
                me03B08Base = EnemyBase as ME03B08Base;
            }
            return me03B08Base;
        }
    }

    [SerializeField] private BasicLaser laserPrefab;
    [SerializeField] private BasicLaser warningPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float warningTime;
    [SerializeField] private int numberLaser;
    [SerializeField] private float durationTime;
    [SerializeField] private float deltaShot;


    [SerializeField, Range(0, 5)] private float radius = 3f;
    [SerializeField, Range(0, 5)] private float warnignRadius = 3f;
    [SerializeField, Range(0, 1)] private float timeOffLaserPercent = 1;
    [SerializeField] private RangeFloatValue randomRadius;
    [SerializeField] private int numberPreload;


    private List<BasicLaser> lasers = new List<BasicLaser>();
    private List<BasicLaser> warnings = new List<BasicLaser>();

    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner warningCountdowner = new Countdowner();

    public override void PreloadIngame() {
        if (laserPrefab) {
            laserPrefab.RegisterPool(numberPreload);
        }
        if (warningPrefab) {
            warningPrefab.RegisterPool(numberPreload);
        }
    }


    public override void Initialize() {
        base.Initialize();
        SpawnLaser();
    }

    public void SetShotDuration(float duration) {
        durationTime = duration;
    }

    private void SpawnLaser() {
        for (int i = lasers.Count; i < numberLaser; ++i) {
            var laser = laserPrefab.Spawn(transform);
            laser.transform.position = firePoint.position;
            laser.SetRadiusSize(radius);
            laser.SetCharacterBase(ME03B08Base);
            laser.gameObject.SetActive(false);
            lasers.Add(laser);
        }

        for (int i = warnings.Count; i < numberLaser; ++i) {
            var warning = warningPrefab.Spawn(transform);
            warning.transform.position = firePoint.position;
            warning.SetRadiusSize(warnignRadius);
            warning.SetCharacterBase(ME03B08Base);
            warning.gameObject.SetActive(false);
            warnings.Add(warning);
        }

        foreach (var l in lasers) {
            l.gameObject.SetActive(false);
        }

        foreach (var w in warnings) {
            w.gameObject.SetActive(false);
        }
    }


    public override bool CanAttack() {
        return true;
    }

    protected override void Attacking() {
        StartWarning();
    }

    public void BeamingLaser() {
        if (warningCountdowner.IsCountdowning()) {
            warningCountdowner.Countdowning(Time.deltaTime);
            if (warningCountdowner.IsTimeOut()) {
                StartBeaming();
            }
        }
        if (durationCountdowner.IsCountdowning()) {
            Beaming();
            durationCountdowner.Countdowning(Time.deltaTime);
            if (durationCountdowner.IsTimeOut()) {
                EndAttack();
            }
        }
    }

    public override void EndAttack() {
        ME03B08Base.EndBossAttack();
        EndBeaming();
        base.EndAttack();
        ME03B08Base.SelfDestruction();
    }

    private void StartWarning() {
        float deltaAngle = 360.0f / numberLaser;
        for (int i = 0; i < numberLaser; ++i) {
            float offset = Random.Range(0, deltaAngle);
            lasers[i].transform.RotateLocalEuler(deltaAngle * i + offset);
            warnings[i].transform.RotateLocalEuler(deltaAngle * i + offset);
        }

        foreach (var warning in warnings) {
            warning.gameObject.SetActive(true);
            warning.StartBeam();
            warning.Beaming(false);
        }
        warningCountdowner.StartCountdown(warningTime);
    }

    private void StartBeaming() {
        foreach (var warning in warnings) {
            warning.gameObject.SetActive(false);
        }

        foreach (var laser in lasers) {
            laser.StartBeam();
            laser.gameObject.SetActive(true);
        }
        deltaShotCountdowner.StartCountdown(deltaShot);
        durationCountdowner.StartCountdown(durationTime);
    }

    private void Beaming() {
        if (deltaShotCountdowner.IsCountdowning()) {
            deltaShotCountdowner.Countdowning(Time.deltaTime);

            float timeOffPoint = durationTime * (1 - timeOffLaserPercent);
            float timeOffElapsed = durationCountdowner.Countdown;
            if (timeOffElapsed < timeOffPoint) {
                float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
                foreach (var laser in lasers) {
                    laser.SetPercentSize(percentSize);

                }
            }

            if (deltaShotCountdowner.IsTimeOut()) {
                foreach (var laser in lasers) {
                    laser.SetRadiusSize(radius * randomRadius.GetRandomValue());
                    laser.SetInfor(ME03B08Base.ME03B08Stat.Atk.Value, null);
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
    }

    private void EndBeaming() {
        foreach (var laser in lasers) {
            laser.EndBeam();
            laser.gameObject.SetActive(false);
        }
    }
}
