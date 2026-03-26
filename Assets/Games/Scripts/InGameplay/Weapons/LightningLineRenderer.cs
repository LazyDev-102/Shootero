

using UnityEngine;

public class LightningLineRenderer : MonoBehaviour {
    [SerializeField] private TargetType[] targetTypes;
    [SerializeField] private EdgeCollider2D collider;
    [SerializeField] private LightningLR lightningLR;

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
            Vector2 start = transform.InverseTransformPoint(startPoint.x, startPoint.y, 0f);
            Vector2 end = transform.InverseTransformPoint(endPoint.x, endPoint.y, 0f);
            Vector2[] points = new Vector2[2];
            points[0] = start;
            points[1] = end;
            collider.points = points;
            lightningLR.UpdatePosition(startPoint, endPoint);
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
                return;
            }
        }
    }

}


