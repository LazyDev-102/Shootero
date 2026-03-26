using Gemmob;
using UnityEngine;

public class SinFrontBullet : FrontBullet {
    [SerializeField] protected float amplitudeBase;
    [SerializeField] protected float cycleBase;
    [SerializeField] protected float startFadeAt;
    private float sinSpeed;
    private float curTime = 0;
    private Vector3 normal = Vector3.forward;
    private Vector3 amplitudeDirection;
    private Vector2 directionShot;
    private Vector2 translatePosition;
    private Vector2 sinPosition;
    private Vector2 prePos = Vector2.zero;
    private Vector2 nextPos = Vector2.zero;
    private int offset;
    private float cAmplitude;
    private float cCyclesPerSecond;

    public override void Initalize() {
        base.Initalize();
    }

    public SinFrontBullet ShootBase(float speed, Vector2 direction, bool r2l = true) {
        this.sinSpeed = SpeedStat.Value + speed;
        transform.up = direction.normalized;
        this.directionShot = direction.normalized;
        this.cAmplitude = amplitudeBase;
        this.cCyclesPerSecond = cycleBase;
        curTime = 0;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        SetR2L(r2l);
        return this;
    }

    public SinFrontBullet Shoot(float speed, Vector2 direction, float amplitude, float cycles, bool r2l = true) {
        this.sinSpeed = SpeedStat.Value + speed;
        transform.up = direction.normalized;
        this.directionShot = direction.normalized;
        this.cAmplitude = amplitude;
        this.cCyclesPerSecond = cycles;
        SetR2L(r2l);
        curTime = 0;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        return this;

    }
    public SinFrontBullet SetR2L(bool r2l) {
        offset = r2l ? 1 : -1;
        return this;
    }
    protected override void FixedUpdate() {
        prePos = transform.position;
        translatePosition = directionShot * sinSpeed * Time.deltaTime;
        sinPosition = (amplitudeDirection * cAmplitude * (Mathf.Sin(cCyclesPerSecond * curTime * 2 * Mathf.PI) - Mathf.Sin(cCyclesPerSecond * (curTime - Time.deltaTime) * 2 * Mathf.PI))) * offset;
        nextPos = MyRigi.position + translatePosition + sinPosition;
        if (double.IsNaN(nextPos.x) || double.IsNaN(nextPos.y)) {
            this.Recycle();
            return;
        }
        MyRigi.MovePosition(nextPos);
        curTime += Time.deltaTime;
        sprite.transform.up = (nextPos - prePos).normalized;
    }
}
