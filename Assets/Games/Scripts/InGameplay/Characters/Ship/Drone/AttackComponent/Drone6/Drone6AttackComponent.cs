using UnityEngine;

public class Drone6AttackComponent : DroneAttackComponent {
    [SerializeField] DroneBase droneBase;
    [SerializeField] Drone06Stat droneStat;
    [SerializeField] BasicLaser bullet;
    [SerializeField, Range(10, 100)] private int laserLength = 20;
    [SerializeField, Range(0f, 5f)] private float radiusSize = 0.5f;
    [SerializeField] private float shotDuration;
    [SerializeField] private float deltaShot;
    [SerializeField] private float rateTime;

    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner rateCd = new Countdowner();
    private Countdowner delayFindAttackCd = new Countdowner();

    private Transform target;
    private bool hasFindTarget;
    private float delayFindAttack = 1f;

    public override void PreloadIngame() {
    }

    public override void Initialize() {
        base.Initialize();
        attackCountdowner.StartCountdown(FireRate);
        StartBeamLaser();
        ChooseTarget();
    }

    public override void PreAttack() {
        base.PreAttack();
        ChooseTarget();
    }

    private void ChooseTarget() {
        target = GameManager.Instance.GameLoader.GetNearestEnemy(transform.position);
    }

    public override void Attack() {
        if (!canAttack) {
            bullet.EndBeam();
            return;
        }

        if (rateCd.IsCountdowning()) {
            rateCd.Countdowning();
            if (rateCd.IsTimeOut()) {
                PreAttack();
            }
        }
        else {
            FindTarget();
            if (!durationCountdowner.IsTimeOut()) {
                durationCountdowner.Countdowning(Time.deltaTime);
                deltaShotCountdowner.Countdowning(Time.deltaTime);
                if (deltaShotCountdowner.IsTimeOut()) {
                    bullet = ChangingLaserBullet(bullet);
                    bullet.Beaming(true);
                    deltaShotCountdowner.StartCountdown(deltaShot);
                }
                else {
                    bullet.Beaming(false);
                }
            }
            else {
                bullet.EndBeam();
                attackCountdowner.Countdowning(Time.deltaTime);
                if (attackCountdowner.IsTimeOut()) {
                    durationCountdowner.StartCountdown(droneStat.LaserDuration.Value);
                    attackCountdowner.StartCountdown(FireRate);
                    rateCd.StartCountdown(rateTime);
                }
            }
        }
    }

    private void OnDisable() {
        EndBeamLaser();
    }

    public override void Updating() {
        base.Updating();
        Attack();
    }

    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(droneStat.LaserDuration.Value);
        deltaShotCountdowner.StartCountdown(deltaShot);
        attackCountdowner.StartCountdown(FireRate);
        rateCd.StartCountdown(rateTime);
        bullet.StartBeam();
        bullet.SetMaxLength(laserLength);
        bullet.SetMaxSize(radiusSize);
        bullet.SetRadiusSize(.2f);
        bullet.gameObject.SetActive(true);
    }

    private void EndBeamLaser() {
        bullet.gameObject.SetActive(false);
        bullet.EndBeam();
    }

    private void FindTarget() {
        if (!hasFindTarget) {
            if (target == null || !target.gameObject.activeInHierarchy) {
                delayFindAttackCd.StartCountdown(delayFindAttack);
                transform.position = droneBase.transform.position;
                transform.localEulerAngles = Vector3.zero;
            }
            else {
                hasFindTarget = true;
                transform.position = droneBase.transform.position;
                transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(droneBase.transform.up, target.transform.position - droneBase.transform.position);
            }
        }
        else {
            if (target != null)
                transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(droneBase.transform.up, target.transform.position - droneBase.transform.position);
            else
                transform.localEulerAngles = Vector3.zero;
        }
    }
}
