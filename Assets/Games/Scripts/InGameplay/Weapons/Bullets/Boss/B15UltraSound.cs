using Gemmob;
using System.Collections;
using UnityEngine;

public class B15UltraSound : MonoBehaviour {
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private ParticleSystem attackEffect;
    private int damage;
    private float deltaShot;
    private B15Base b15Base;
    private HitInfor hitboxInfor;

    public void SetInfo(int damage, float deltaShot, B15Base b15Base) {
        this.damage = damage;
        this.deltaShot = deltaShot;
        this.b15Base = b15Base;
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
        hitboxInfor.SetInfor(damage, null, b15Base);
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
        myCollider.transform.localPosition = Vector3.zero;
        myCollider.transform.localEulerAngles = Vector3.zero;
        myCollider.enabled = true;
        yield return Yielder.Wait(deltaShot);
        myCollider.enabled = false;
    }
}
