using UnityEngine;
using Gemmob;

public class Drone2Bullet : BulletBase {
    private float speed;
    private float accelerationSpeed = 0f;
    [SerializeField] private float timeLife = 3;
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] protected Transform objectRotate;
    [SerializeField] protected bool isRotation;
    [SerializeField] protected CircleCollider2D col;
    [SerializeField] protected Drone2Explosion droneExplosion;
    [SerializeField, Range(0f, 10f)] protected float radiusBoom = 5f;
    private Vector2 direction;
    private bool Dead;
    Countdowner timeCountdown = new Countdowner();

    protected override void OnEnable() {
        base.OnEnable();
        Dead = false;
        timeCountdown.StartCountdown(timeLife);
    }
    public void Shoot(float speed, Vector2 direction, float acceleration = 0f) {
        this.speed = speed + SpeedStat.Value;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
    }

    public void Shoot(float speed, Quaternion rotation) {
        this.speed = speed;
        transform.rotation = rotation;
        this.direction = transform.up;
    }

    private void FixedUpdate() {
        if (Dead)
            return;
        myRigi.MovePosition(myRigi.position + direction * speed * Time.fixedDeltaTime);
        speed += accelerationSpeed * Time.fixedDeltaTime;
        if (isRotation) {
            var temp = objectRotate.rotation;
            temp.z += Time.deltaTime;
            objectRotate.rotation = temp;
        }
        timeCountdown.Countdowning(Time.deltaTime);
        if (timeCountdown.IsTimeOut()) {
            Dead = true;
            SpawnBoomExplosion();
            Destroy();
        }
    }

    protected override void Hit(Collider2D collision) {
        isHitted = true;
        GetComponent<Collider2D>().enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
            SpawnBoomExplosion();
        }
        DestroyWithEffect();
    }
    private void SpawnBoomExplosion() {
        Drone2Explosion newExplosion = droneExplosion.Spawn();
        newExplosion.transform.position = transform.position;
        newExplosion.InitData(new EventKey.ExplosionObject() { Position = transform.position, radius = radiusBoom, Damage = HitInfor.Damage.Value / 2, Causer = HitInfor.Causer });
    }
}


//public class ExplosionObject : MonoBehaviour {

//    private HitInfor myHit;

//    public void SetDamage(int d, ObjectBase c) {
//        myHit = new HitInfor();
//        myHit.SetInfor(d, null, c);
//    }
//}
