using UnityEngine;

public class MB13Skill01AttackComponent : MinibossAttackComponent<MB13Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Laser bullet1;
    [SerializeField] private Laser warningLine1;

    [SerializeField] private Laser bullet2;
    [SerializeField] private Laser warningLine2;

    [SerializeField] private float damagePercent;
    [SerializeField] private float duration;
    [SerializeField] private float deltaShot;
    [SerializeField, Range(0f, 100f)] private float bulletSpeed = 10f;
    [SerializeField, Range(0, 180)] private float angleStart = 30;
    [SerializeField, Range(0, 5)] private float radius = 0.5f;
    [SerializeField, Range(0, 1)] private float timeOffLaserPercent = 1;
    [SerializeField, Range(0f, 1f)] private float timeOffWarningLaserPercent = 0.5f;
    [SerializeField] private int warningMaxStack = 4;
    [SerializeField, Range(0f, 1f)] private float warningAlpha = 0.5f;

    private Countdowner rateCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();
    private float timeOffPoint;
    private float warningTimeOffPoint;
    private int warningStack = 0;


    public override void StartAttack() {
        gameObject.SetActive(true);
        InitAngleBullet();
        StartBeamLaser();
        bullet1.SetRadiusSize(radius);
        bullet2.SetRadiusSize(radius);
        warningLine1.StartBeam();
        warningLine2.StartBeam();
        warningLine1.SetAlphaLaser(warningAlpha);
        warningLine2.SetAlphaLaser(warningAlpha);
    }
    private void InitAngleBullet() {
        var temp = bullet1.transform.localEulerAngles;
        var temp2 = bullet1.transform.localEulerAngles;
        temp.z = angleStart;
        temp2.z = -angleStart;
        bullet1.transform.localEulerAngles = temp;
        bullet2.transform.localEulerAngles = temp2;
        warningLine1.transform.localEulerAngles = temp;
        warningLine2.transform.localEulerAngles = temp2;
    }
    private void DrawWarning() {
        if (delayCountdowner.Countdown < warningTimeOffPoint) {
            float percentSize = warningTimeOffPoint == 0 ? 1 : delayCountdowner.Countdown / warningTimeOffPoint;
            warningLine1.SetPercentSize(percentSize);
            warningLine2.SetPercentSize(percentSize);
            if (warningStack % warningMaxStack == 0) {
                warningLine1.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
                warningLine2.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
            }
            warningStack++;

        }
        warningLine1.gameObject.SetActive(true);
        warningLine1.Beaming(false);

        warningLine2.gameObject.SetActive(true);
        warningLine2.Beaming(false);
    }

    private void HideWarning() {
        warningLine1.gameObject.SetActive(false);
        warningLine2.gameObject.SetActive(false);

    }

    public override void Updating() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            DrawWarning();
            minibossAttack.MB13Base.LookTarget();
            if (delayCountdowner.IsTimeOut()) {
                HideWarning();
            }
        }
        else {

            BeamingLaser();
        }
    }
    public override void Attacking() {
        //StartBeamLaser();
    }
    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(duration);
        deltaShotCountdowner.StartCountdown(deltaShot);
        rateCountdowner.StartCountdown(delayAttack);
        delayCountdowner.StartCountdown(delayAttack);
        bullet1.StartBeam();
        bullet2.StartBeam();
        bullet1.gameObject.SetActive(true);
        bullet2.gameObject.SetActive(true);
        timeOffPoint = duration * (1 - timeOffLaserPercent);
        warningTimeOffPoint = delayAttack * (1 - timeOffWarningLaserPercent);
    }



    public void BeamingLaser() {
        if (!durationCountdowner.IsTimeOut()) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);

            float timeOffElapsed = durationCountdowner.Countdown;
            if (timeOffElapsed < timeOffPoint) {
                float percentSize = timeOffPoint == 0 ? 1 : timeOffElapsed / timeOffPoint;
                bullet1.SetPercentSize(percentSize);
                bullet2.SetPercentSize(percentSize);

            }

            Rotation();
            if (deltaShotCountdowner.IsTimeOut()) {
                bullet1.SetInfor((int)(minibossAttack.MB13Base.MB13Stat.Atk.Value * damagePercent), null);
                bullet2.SetInfor((int)(minibossAttack.MB13Base.MB13Stat.Atk.Value * damagePercent), null);
                bullet1.Beaming(true);
                bullet2.Beaming(true);
                deltaShotCountdowner.StartCountdown(deltaShot);
            }
            else {
                bullet1.Beaming(false);
                bullet2.Beaming(false);
            }
        }
        else {
            rateCountdowner.Countdowning(Time.deltaTime);
            if (rateCountdowner.IsTimeOut()) {
                durationCountdowner.StartCountdown(duration);
                rateCountdowner.StartCountdown(delayAttack);
            }
            EndAttack();
        }
    }
    private void Rotation() {
        var temp = bullet1.transform.localEulerAngles;
        var temp2 = bullet2.transform.localEulerAngles;
        if (temp.z > angleStart + 1)
            return;
        temp.z -= Time.deltaTime * bulletSpeed;
        temp2.z += Time.deltaTime * bulletSpeed;
        bullet1.transform.localEulerAngles = temp;
        bullet2.transform.localEulerAngles = temp2;
    }
    private void EndBeamLaser() {
        bullet1.EndBeam();
        bullet2.EndBeam();
        bullet1.gameObject.SetActive(false);
        bullet2.gameObject.SetActive(false);
        warningLine1.gameObject.SetActive(false);
        warningLine2.gameObject.SetActive(false);
    }

    public override void EndAttack() {
        base.EndAttack();
        EndBeamLaser();
        gameObject.SetActive(false);
    }

    public override void StopAttack() {
        base.StopAttack();
        EndBeamLaser();
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        EndBeamLaser();
    }
}
