using UnityEngine;

public class OneLaserTarget : MonoBehaviour {
    [SerializeField] private BasicLaser laser;

    private ShipBase ship;
    private GameLoader loader;
    private bool hasFindTarget;
    private int damage = 0;
    private float fireRate = 5f;
    private float duration = 0.1f;
    private float delayFindAttack = 1f;
    private float deltaShot = 0.1f;
    private Countdowner fireRateCd = new Countdowner();
    private Countdowner durationCd = new Countdowner();
    private Countdowner deltaShotCd = new Countdowner();
    private Countdowner delayFindAttackCd = new Countdowner();

    public void Init(ShipBase ship, float fireRate, float duration, float deltaShot, float percentDamage) {
        this.ship = ship;
        this.fireRate = fireRate;
        this.duration = duration;
        this.deltaShot = deltaShot;
        damage = (int)(ship.ShipStat.Atk.Value * percentDamage);
        durationCd.StartCountdown(duration);
        //timeOffPoint = duration * 0.2f;
        laser.SetPercentSize(1);
        loader = GameManager.Instance.GameLoader;
    }

    public void Updating() {
        if (fireRateCd.IsTimeOut()) {
            if (durationCd.IsCountdowning()) {
                durationCd.Countdowning(Time.deltaTime);
                BeamingLaser();
            }
            else {
                hasFindTarget = false;
                laser.EndBeam();
                delayFindAttackCd.StartCountdown(0);
                fireRateCd.StartCountdown(fireRate);
                durationCd.StartCountdown(duration);
            }
        }
        else {
            fireRateCd.Countdowning(Time.deltaTime);
        }
    }

    public void BeamingLaser() {
        FindTarget();
        Shot();
    }

    private void FindTarget() {
        if (!hasFindTarget) {
            if (loader.EnemyCount() == 0) {
                delayFindAttackCd.StartCountdown(delayFindAttack);
                transform.position = ship.transform.position;
                transform.localEulerAngles = Vector3.zero;
            }
            else {
                hasFindTarget = true;
                transform.position = ship.transform.position;
                transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(ship.transform.up, loader.Enemies[0].transform.position - ship.transform.position);
            }
        }
        else {
            if (loader.EnemyCount() != 0)
                transform.localEulerAngles = Vector3.forward * Vector2.SignedAngle(ship.transform.up, loader.Enemies[0].transform.position - ship.transform.position);
        }

        return;
    }
    private void Shot() {
        if (deltaShotCd.IsTimeOut()) {
            laser.SetInfor(damage, null);
            laser.Beaming(true);
            deltaShotCd.StartCountdown(deltaShot);
        }
        else {
            laser.Beaming(false);
            deltaShotCd.Countdowning(Time.deltaTime);
        }
    }
}
