using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;

public class HomingSinBullet : SinBullet {
    [SerializeField] private float turn;
    [SerializeField] private float delayHoming;
    [SerializeField] private float timeHoming;
    [SerializeField] private bool isPauseHoming;
    private bool isHoming;
    private float countdownHoming;
    private float acceler;

    private Transform myTransform;
    private Transform target;

    protected override void OnEnable() {
        base.OnEnable();
        myTransform = transform;
    }

    public void Shoot(float speed, Transform target, Vector2 direction, float acceler = 0) {
        this.speed = speed + SpeedStat.Value;
        this.target = target;
        isHoming = false;
        this.acceler = acceler;
        if (Mathf.Abs(Vector2.SignedAngle(direction, Vector2.down)) < 2) {
            direction = UnityHelper.Down;
        }
        myTransform.up = direction;
        if (isPauseHoming) {
            MyRigi.velocity = Vector2.zero;
        }
        else {
            MyRigi.velocity = myTransform.up * speed;
        }
        if (gameObject.activeInHierarchy)
            StartCoroutine(HoldHoming());
    }

    public void SetLifeTimeHoming(float lifeTime) {
        this.timeHoming = lifeTime;
    }

    public void SetDelayHoming(float delayHoming) {
        this.delayHoming = delayHoming;
    }

    private IEnumerator HoldHoming() {
        yield return Yielder.Wait(delayHoming);
        countdownHoming = timeHoming;
        isHoming = true;
    }



    protected override void FixedUpdate() {
        if (isHoming && target != null && countdownHoming > 0) {
            MyRigi.velocity = myTransform.up * speed;
            Vector3 targetVector = target.position - myTransform.position;
            float rotatingIndex = Vector3.Cross(targetVector, myTransform.up).z;
            MyRigi.angularVelocity = -1 * rotatingIndex * turn;
            speed += acceler * Time.deltaTime;
            countdownHoming -= Time.fixedDeltaTime;
        }
        else {
            if (isPauseHoming && !isHoming && target != null) {
                Vector3 targetVector = target.position - myTransform.position;
                MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, Vector2.SignedAngle(Vector2.up, targetVector), Time.deltaTime * turn));
            }
            else {
                MyRigi.angularVelocity = 0;
                myTransform.up = MyRigi.velocity;
            }
        }
    }
}
