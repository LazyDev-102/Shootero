using Gemmob;
using Helper;
using UnityEngine;

public class SinHomingBullet : HomingBullet {

    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float cyclesPerSecond = 1f;

    float curTime = 0;
    private Vector3 normal = Vector3.forward;
    private Vector3 amplitudeDirection;
    private Vector2 direction;

    private Vector2 translatePosition;
    private Vector2 sinPosition;
    private Vector2 prePos = Vector2.zero;
    private Vector2 nextPos = Vector2.zero;
    private int offset;
    private float originAmplitude;
    private float originCyclesPerSecond;
    private bool isLoad;

    public override void Initalize() {
        base.Initalize();
        if (!isLoad) {
            isLoad = true;
            originAmplitude = amplitude;
            originCyclesPerSecond = cyclesPerSecond;
        }
        else {
            amplitude = originAmplitude;
            cyclesPerSecond = originCyclesPerSecond;
        }
    }
    public new SinHomingBullet Shoot(float speed, Transform target, Vector2 direction, float acceler = 0, bool r2l = true) {
        this.speed = speed + SpeedStat.Value;
        this.target = target;
        this.direction = direction.normalized;
        this.acceler = acceler;
        transform.up = direction.normalized;
        curTime = 0;
        isHoming = false;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        if (Mathf.Abs(Vector2.SignedAngle(direction, Vector2.down)) < 2) {
            direction = UnityHelper.Down;
        }
        myTransform.up = direction;
        myRigi.velocity = myTransform.up * speed;
        SetR2L(r2l);
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (ship)
            SetInfo(ship);
        if (gameObject.activeInHierarchy)
            StartCoroutine(HoldHoming());
        return this;
    }

    public virtual SinHomingBullet Shoot(float speed, Transform target, Vector2 direction, float amplitude, float cycles, float acceler = 0, bool r2l = true) {
        this.speed = speed + SpeedStat.Value;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.amplitude = amplitude;
        this.cyclesPerSecond = cycles;
        this.target = target;
        this.acceler = acceler;
        curTime = 0;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        SetR2L(r2l);
        isHoming = false;
        if (Mathf.Abs(Vector2.SignedAngle(direction, Vector2.down)) < 2) {
            direction = UnityHelper.Down;
        }
        myTransform.up = direction;
        myRigi.velocity = myTransform.up * speed;
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (ship)
            SetInfo(ship);
        if (gameObject.activeInHierarchy)
            StartCoroutine(HoldHoming());
        return this;

    }
    public SinHomingBullet SetR2L(bool r2l) {
        offset = r2l ? 1 : -1;
        return this;
    }

    protected override void FixedUpdate() {
        prePos = transform.position;
        if (isHoming && target != null && countdownHoming > 0) {
            myRigi.velocity = myTransform.up * speed;
            Vector3 targetVector = target.position - myTransform.position;
            float rotatingIndex = Vector3.Cross(targetVector, myTransform.up).z;
            myRigi.angularVelocity = -1 * rotatingIndex * turn;
            speed += acceler * Time.deltaTime;
            countdownHoming -= Time.fixedDeltaTime;
        }
        else {
            translatePosition = direction * speed * Time.deltaTime;
            sinPosition = (amplitudeDirection * amplitude * (Mathf.Sin(cyclesPerSecond * curTime * 2 * Mathf.PI) - Mathf.Sin(cyclesPerSecond * (curTime - Time.deltaTime) * 2 * Mathf.PI))) * offset;
            nextPos = MyRigi.position + translatePosition + sinPosition;
            if (double.IsNaN(nextPos.x) || double.IsNaN(nextPos.y)) {
                this.Recycle();
                return;
            }
            MyRigi.MovePosition(nextPos);
            curTime += Time.deltaTime;
        }
        sprite.transform.up = (nextPos - prePos).normalized;
    }
}
