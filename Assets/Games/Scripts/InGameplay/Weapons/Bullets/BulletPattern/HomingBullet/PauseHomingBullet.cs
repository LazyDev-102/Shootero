
using Gemmob;
using System.Collections;
using UnityEngine;

public class PauseHomingBullet : BulletBase {
    [SerializeField] private float turn;
    [SerializeField] private float delayHoming;
    [SerializeField] private float timeHoming;
    [SerializeField] private Rigidbody2D myRigi;
    private float speed;
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
        myTransform.up = direction;
        myRigi.velocity = myTransform.up * speed;
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



    protected virtual void FixedUpdate() {
        if (isHoming && target != null && countdownHoming > 0) {
            myRigi.velocity = myTransform.up * speed;
            Vector3 targetVector = target.position - myTransform.position;
            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
            myRigi.angularVelocity = -1 * rotatingIndex * turn;
            countdownHoming -= Time.fixedDeltaTime;
        }
        else {
            myRigi.angularVelocity = 0;
            myTransform.up = myRigi.velocity;
        }
    }
}
