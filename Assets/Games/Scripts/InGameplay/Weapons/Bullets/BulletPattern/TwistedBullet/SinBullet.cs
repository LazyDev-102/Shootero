

using UnityEngine;

public class SinBullet : BulletBase {
    protected float speed;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float cyclesPerSecond = 1f;

    //ship 06
    [SerializeField] protected float startFadeAt;

    float curTime = 0;
    private Vector3 normal = Vector3.forward;
    private Vector3 amplitudeDirection;
    private Vector2 direction;

    private Vector2 translatePosition;
    private Vector2 sinPosition;
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

    public virtual void SetTimeFading(float time) {
    }

    public SinBullet Shoot(float speed, Vector2 direction, bool r2l = true) {
        this.speed = speed;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        curTime = 0;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        SetR2L(r2l);
        return this;
    }

    public virtual SinBullet Shoot(float speed, Vector2 direction, float amplitude, float cycles, bool r2l = true) {
        this.speed = speed;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.amplitude = amplitude;
        this.cyclesPerSecond = cycles;
        SetR2L(r2l);
        curTime = 0;
        amplitudeDirection = Vector3.Cross(normal, direction).normalized;
        return this;

    }
    public SinBullet SetR2L(bool r2l) {
        offset = r2l ? 1 : -1;
        return this;
    }

    protected virtual void FixedUpdate() {
        translatePosition = direction * speed * Time.deltaTime;
        sinPosition = (amplitudeDirection * amplitude * (Mathf.Sin(cyclesPerSecond * curTime * 2 * Mathf.PI) - Mathf.Sin(cyclesPerSecond * (curTime - Time.deltaTime) * 2 * Mathf.PI))) * offset;
        MyRigi.MovePosition(MyRigi.position + translatePosition + sinPosition);
        curTime += Time.deltaTime;
    }
}
