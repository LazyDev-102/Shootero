using Gemmob;
using System.Collections;
using UnityEngine;

public class E17BulletAttack : MonoBehaviour {
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private ParticleSystem attackEffect;
    private int damage;
    private float deltaShot;
    private E17Base e17Base;
    private HitInfor hitboxInfor;

    public void SetInfo(int damage, float deltaShot, E17Base e17Base) {
        this.damage = damage;
        this.deltaShot = deltaShot;
        this.e17Base = e17Base;
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag(GameTag.Player)) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
            }
        }
    }
    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, e17Base);
        return hitboxInfor;
    }
    public void TurnEffect(bool turnOn) {
        gameObject.SetActive(turnOn);
        if (attackEffect) {
            if (turnOn)
                attackEffect.Play();
            else
                attackEffect.Stop();
        }
    }
    public IEnumerator IShotting() {
        //gameObject.SetActive(true);
        myCollider.transform.localPosition = Vector3.zero;
        myCollider.transform.localEulerAngles = Vector3.zero;
        myCollider.enabled = true;
        yield return Yielder.Wait(deltaShot);
        myCollider.enabled = false;
        //gameObject.SetActive(false);
    }
}
