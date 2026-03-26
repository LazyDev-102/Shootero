

using UnityEngine;

public class LightningLine : MonoBehaviour {
    [SerializeField] private TargetType[] targetTypes;
    [SerializeField] private EdgeCollider2D collider;
    [SerializeField] private LightningLineBolt lineBolt;


    private HitInfor hitInfor;
    private bool isActive;
    private Vector3 posCollider;

    public void SetActive(bool active) {
        isActive = active;
        gameObject.SetActive(active);
    }

    public void UpdatePosition(Vector2 startPoint, Vector2 endPoint) {
        if (isActive) {
            posCollider = (startPoint + endPoint) / 2f;
            collider.transform.position = posCollider;
            Vector2[] points = new Vector2[2];
            points[0] = collider.transform.InverseTransformPoint(startPoint);
            points[1] = collider.transform.InverseTransformPoint(endPoint);
            collider.points = points;
            lineBolt.Draw(startPoint, endPoint);
        }
    }
    public void SetInfor(int damage, ObjectBase causer) {
        if (hitInfor == null) {
            hitInfor = new HitInfor();
        }
        hitInfor.SetInfor(damage, null, causer);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isActive) {
            return;
        }
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                IHitbox victim = collision.GetComponent<IHitbox>();
                if (victim != null) {
                    victim.TakeHit(hitInfor, transform.position);
                }
                SetDataBeforeFreeze(collision);
                return;
            }
        }
    }
    private void Update() {
        ReloadFreezeHitDamage();
    }

    private float hitDamageCountdown = 0.5f;
    private bool isFreezeHitDame;
    private float freezeDuration = 0f;
    private void ReloadFreezeHitDamage() {
        if (isFreezeHitDame) {
            freezeDuration += Time.deltaTime;
            if (freezeDuration > hitDamageCountdown) {
                SetDataAfterFreeze();
            }
        }
    }
    private void SetDataBeforeFreeze(Collider2D targetCollider) {
        if (targetCollider.tag.Equals(GameTag.Player)) {
            collider.enabled = false;
            isFreezeHitDame = true;
        }
    }
    private void SetDataAfterFreeze() {
        collider.enabled = true;
        isFreezeHitDame = false;
        freezeDuration = 0;
    }
}
