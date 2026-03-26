using Gemmob;
using System.Collections;
using UnityEngine;

public class MB13Skill02AttackComponent : MinibossAttackComponent<MB13Attack> {
    [SerializeField] private Laser bullet;
    [SerializeField] private Laser waring;
    [SerializeField, Range(0f, 1f)] private float timeOffLaserPercent = 1;

    [SerializeField] private float radius;

    [SerializeField] private float delayTime;
    [SerializeField] private float timeLife;
    [SerializeField] private int stackPerAttack = 5;
    [SerializeField] private int attackCount = 2;
    [SerializeField] private float timePerShot = 1;
    [SerializeField] private float timePerAttack = 2;
    [SerializeField] private int startAnglePoint = -30;
    [SerializeField] private int endAnglePoint = 30;
    [SerializeField] private float damagePercent = 1;
    private bool attacking;
    private bool endAttack;
    private bool hasRotate;

    private Countdowner delayCountdowner = new Countdowner();

    public override void StartAttack() {
        bullet.SetRadiusSize(radius);
        attacking = false;
        endAttack = false;
    }
    public override void Updating() {
        if (!attacking) {
            minibossAttack.MB13Base.MB13Move.MoveDirect();
            Rotation();
            attacking = minibossAttack.MB13Base.MB13Move.CompleteMoveToTarget();
        }
        else {
            BeamingLaser();
        }
    }

    private void HideWarning() {
        waring.gameObject.SetActive(false);
    }

    public override void Attacking() {
        StartBeamLaser();
        SetTimeCountdown();
        minibossAttack.MB13Base.MB13Move.StartMoveAfterAttackMB13(new Vector2(0.5f, 0.8f));
    }
    private void SetTimeCountdown() {
        delayCountdowner.StartCountdown(delayTime);
    }
    private void StartBeamLaser() {
        bullet.StartBeam();
        bullet.gameObject.SetActive(true);
    }

    public void BeamingLaser() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            //Rotation();
        }
        else {
            if (!endAttack) {
                hasRotate = false;
                endAttack = true;
                if (gameObject.activeInHierarchy)
                    StartCoroutine(BulletAttack());
            }
        }
    }

    private IEnumerator BulletAttack() {
        for (int i = 0; i < attackCount; i++) {
            waring.gameObject.SetActive(true);
            var time = 0f;
            while (time < timePerAttack) {
                time += Time.deltaTime;
                minibossAttack.MB13Base.LookTarget();
                yield return null;
            }
            waring.gameObject.SetActive(false);
            for (int j = 0; j < stackPerAttack; j++) {
                bullet.gameObject.SetActive(true);
                bullet.SetPercentSize(1);
                var temp = bullet.transform.localEulerAngles;
                temp.z = Random.Range(startAnglePoint, endAnglePoint);
                bullet.transform.localEulerAngles = temp;
                bullet.SetInfor((int)(minibossAttack.MB13Base.MB13Stat.Atk.Value * damagePercent), null);
                bullet.Beaming(true);
                float timeOffPoint = timeLife * (1 - timeOffLaserPercent);
                float timeOffPoint1 = timeLife * (1 - timeOffLaserPercent);
                yield return Yielder.Wait(timeLife - timeOffPoint);
                while (timeOffPoint > 0) {
                    bullet.SetPercentSize(timeOffPoint / timeOffPoint1);
                    bullet.Beaming(false);
                    timeOffPoint -= Time.deltaTime;
                    yield return null;
                }
                bullet.gameObject.SetActive(false);
                yield return Yielder.Wait(timePerShot);
            }
        }

        EndAttack();
        EndBeamLaser();
    }
    private void Rotation() {
        minibossAttack.transform.localEulerAngles = Vector3.Lerp(minibossAttack.transform.localEulerAngles, Vector3.forward * 180, 0.125f);
    }
    private void EndBeamLaser() {
        bullet.gameObject.SetActive(false);
        waring.gameObject.SetActive(false);
        bullet.EndBeam();
    }

    public override void StopAttack() {
        HideWarning();
        base.StopAttack();
    }

    private void OnDisable() {
        EndBeamLaser();
    }
}
