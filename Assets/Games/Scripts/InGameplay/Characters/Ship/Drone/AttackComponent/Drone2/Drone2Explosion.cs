using Gemmob;
using UnityEngine;

public class Drone2Explosion : MonoBehaviour {
    [SerializeField] private float timeLife = 0.5f;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private TargetType[] targetTypes;
    private HitInfor myHit;
    private bool Dead;
    Countdowner timeCountdown = new Countdowner();

    private void Awake() {
        col = GetComponent<CircleCollider2D>();
        this.AddListener<EventKey.ExplosionObject>(InitData, false);
    }

    public void InitData(EventKey.ExplosionObject data) {
        transform.position = data.Position;
        col.radius = data.radius;
        SetDamage(data.Damage, data.Causer);
        gameObject.SetActive(true);
        if (explosion != null)
            explosion.Play();
    }

    public void SetDamage(int d, ObjectBase c) {
        myHit = new HitInfor();
        myHit.SetInfor(d, null, c);
    }
    private void OnEnable() {
        timeCountdown.StartCountdown(timeLife);
        Dead = false;
    }

    private void FixedUpdate() {
        if (Dead)
            return;
        timeCountdown.Countdowning(Time.deltaTime);
        if (timeCountdown.IsTimeOut()) {
            Dead = true;
            gameObject.Recycle();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                IHitbox victim = collision.GetComponent<IHitbox>();
                if (victim != null) {
                    victim.TakeHit(myHit, transform.position);
                }
                return;
            }
        }
    }
}
