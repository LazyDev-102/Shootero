using System.Collections;
using UnityEngine;

public class SatelliteManager : MonoBehaviour {
    [SerializeField] private ShipBase ship;
    [SerializeField] private ShipSatellite[] satellites;
    [SerializeField] private float rotateSpeed;

    Transform myTransform;
    private float speedAutomatic;
    private float delayperAttackAutomatic;
    private bool canAttackAutomaticSatellite;
    private bool attakingAutomaticSatellite;
    private Countdowner attackAutomaticSatelliteCD = new Countdowner();
    private void Awake() {
        myTransform = transform;
    }
    public void InitData(float rotateSpeed, float distanceWithShip) {
        this.rotateSpeed = rotateSpeed;
        foreach (var s in satellites) {
            s.SatelliteRangeSetBaseValue(distanceWithShip);
        }
    }
    public void Initialize() {
        foreach (var s in satellites) {
            s.SetShip(ship);
        }
    }
    public void EnableSatelliteQuantity() {
        foreach (var item in satellites) {
            item.gameObject.SetActive(true);
        }
    }
    public void EnableAssault(float percentDamage) {
        foreach (var s in satellites) {
            s.EnableAssault(percentDamage);
        }
    }
    public void EnableAutomaticSatellite(float speed, float delayPerAttack, float percentDamage) {
        this.speedAutomatic = speed;
        this.delayperAttackAutomatic = delayPerAttack;
        canAttackAutomaticSatellite = true;
        attakingAutomaticSatellite = false;
        attackAutomaticSatelliteCD.StartCountdown(0);
        foreach (var s in satellites) {
            s.ChangeColliderDamage(percentDamage);
        }
    }
    private IEnumerator AttackAutomaticSatellite() {
        attakingAutomaticSatellite = true;
        ShipSatellite s;
        var loopTime = 0;
        do {
            s = satellites[Random.Range(0, satellites.Length)];
            loopTime++;
            if (loopTime > 10) {
                s = satellites[0];
                break;
            }
        } while (!s.gameObject.activeInHierarchy);
        yield return StartCoroutine(s.EnableAutomatic(speedAutomatic));
        attakingAutomaticSatellite = false;
    }
    public void EnableFireBallSatellite() {
        foreach (var s in satellites) {
            s.EnableFireBall();
        }
    }
    public void EnableSatelliteRange(float[] range) {
        foreach (var s in satellites) {
            s.EnableSatelliteRange(range);
        }
    }

    public void ChangeAutomaticDeltaAttack(float percent) {
        this.delayperAttackAutomatic *= (1 - percent);
    }
    public void ChangeRotateSpeed(float percent) {
        rotateSpeed *= (1 + percent);
    }
    private void Update() {
        myTransform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
        if (canAttackAutomaticSatellite) {
            if (!attakingAutomaticSatellite) {
                if (attackAutomaticSatelliteCD.IsTimeOut()) {
                    if (gameObject.activeInHierarchy)
                        StartCoroutine(AttackAutomaticSatellite());
                    attackAutomaticSatelliteCD.StartCountdown(delayperAttackAutomatic);
                }
                else {
                    attackAutomaticSatelliteCD.Countdowning(Time.deltaTime);
                }
            }
        }
    }
}
