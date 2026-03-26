
using Gemmob;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]

public class Ship07LightningLineBolt : MonoBehaviour {
    [SerializeField] private LightningBolt[] lightningBolts;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float lifeTime = 0.1f;
    private int bounceCount;
    private HitInfor hit;

    public void SetStartPoint(Transform s) {
        startPoint = s;
    }

    public void SetEndPoint(Transform e) {
        endPoint = e;
    }

    public void Draw() {
        foreach (var b in lightningBolts) {
            if (b != null && startPoint != null && endPoint != null)
                b.DrawLightning(startPoint.position, endPoint.position);
        }
    }

    public void Draw(Vector2 startPoint, Vector2 endPoint) {
        foreach (var b in lightningBolts) {
            if (b != null)
                b.DrawLightning(startPoint, endPoint);
        }
    }

#if UNITY_EDITOR
    public bool play;

    private void Update() {
        if (play) {
            Draw();
        }
    }
#endif
    public (bool, GameObject) FindNewTarget(EnemyBase currentE) {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            return (false, null);
        }
        var radius = 5f;
        var layer = LayerMask.GetMask(GameLayer.Enemy);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(endPoint.position, radius, Vector2.up, 1f, layer);
        for (int i = 0; i < hits.Length; i++) {
            var result = hits[i].collider.GetComponent<EnemyBase>();
            if (result != null && result != currentE) {
                return (true, hits[i].collider.gameObject);
            }
        }
        return (false, null);
    }
    public void SetPosition(Ship07LightningLineBolt item, Transform source, Transform destination) {
        item.SetStartPoint(source);
        item.SetEndPoint(destination);
        gameObject.SetActive(true);
        StartCoroutine(Survival());
    }
    public void SetBounceCount(int bounceCount) {
        this.bounceCount = bounceCount;
    }
    public void SetHitInfo(HitInfor hit) {
        this.hit = hit;
    }
    public IEnumerator Survival() {
        gameObject.SetActive(true);
        float time = 0f;
        while (time < lifeTime) {
            time += Time.deltaTime;
            Draw();
            yield return null;
        }
        if (bounceCount > 0) {
            bounceCount--;
            (bool next, GameObject eNext) = FindNewTarget((EnemyBase)hit.Causer);
            var e = eNext == null ? null : eNext.GetComponent<EnemyBase>();
            if (next && e != null) {
                e.EnemyHitbox.TakeHitDamage(hit, transform.position);
                SetPosition(this, endPoint, e.transform);
                hit.SetInfor(hit.Damage.Value, hit.Effects, e, hit.CritChance, hit.CritDamage);
                StartCoroutine(Survival());
            }
            else {
                this.Recycle();
            }
        }
        else
            this.Recycle();
    }
}
