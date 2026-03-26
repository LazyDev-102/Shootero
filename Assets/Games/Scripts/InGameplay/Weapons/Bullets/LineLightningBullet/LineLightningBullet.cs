using UnityEngine;

public class LineLightningBullet : BulletBase {
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] private MiniLighiningCircle[] lightningCircles;
    [SerializeField] private MiniLightningLine lightningLine;

    private float speed;
    private float accelerationSpeed = 0f;
    private Vector2 direction;

    public override void Initalize() {
        base.Initalize();
        foreach (var circle in lightningCircles) {
            circle.gameObject.SetActive(true);
            circle.Initalize();
            circle.AddOnDestroy(OnCircleDestroy);
        }
        lightningLine.gameObject.SetActive(true);
        lightningLine.Initalize();
    }

    public void SetInfor(int damageCircle, int damageLine, ObjectBase causer) {
        foreach (var circle in lightningCircles) {
            circle.SetHitInfor(damageCircle, null, HitInfor.Causer);
        }
        lightningLine.SetHitInfor(damageLine, null, HitInfor.Causer);
    }

    public void Shoot(float speed, Vector2 direction, float acceleration = 0f) {
        this.speed = speed;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
    }

    private void FixedUpdate() {
        myRigi.MovePosition(myRigi.position + direction * speed * Time.fixedDeltaTime);
        speed += accelerationSpeed * Time.fixedDeltaTime;
    }

    private void OnCircleDestroy(Vector3 position) {
        lightningLine.gameObject.SetActive(false);
        foreach (var circle in lightningCircles) {
            circle.RemoveOnDestroy(OnCircleDestroy);
        }
    }

    protected override bool IsBlockHit() {
        return true;
    }
}
