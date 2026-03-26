using Gemmob;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]

public class LightningLineBolt : MonoBehaviour {
    [SerializeField] private LightningBolt[] lightningBolts;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    public void SetStartPoint(Transform s) {
        startPoint = s;
    }

    public void SetEndPoint(Transform e) {
        endPoint = e;
    }

    public void Draw() {
        foreach (var b in lightningBolts) {
            b.DrawLightning(startPoint.position, endPoint.position);
        }
    }

    public void Draw(Vector2 startPoint, Vector2 endPoint) {
        foreach (var b in lightningBolts) {
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
    public (bool, GameObject) FindNewTarget(EnemyBase currentE, LayerMask enemyMask) {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            return (false, null);
        }
        var radius = 5f;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 5f, enemyMask);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider.GetComponent<EnemyBase>() != currentE) {
                return (true, hits[i].collider.gameObject);
            }
        }
        return (false, null);
    }
    public void SetPosition(LightningLineBolt item, Transform source, Transform destination) {
        item.SetStartPoint(source);
        item.SetEndPoint(destination);
    }
    public IEnumerator Survival(int bounceCount, HitInfor hit) {
        gameObject.SetActive(true);
        float time = 0f;
        while (time < 0.1f) {
            time += Time.deltaTime;
            Draw();
            yield return null;
        }
        if (bounceCount > 0) {
            bounceCount--;
            (bool next, GameObject eNext) = FindNewTarget((EnemyBase)hit.Causer, LayerMask.NameToLayer(GameLayer.Enemy));
            var e = eNext.GetComponent<EnemyBase>();
            if (next && e != null) {
                e.EnemyHitbox.TakeHitDamage(hit, transform.position);
                SetPosition(this, transform, e.transform);
                hit.SetInfor(hit.Damage.Value, hit.Effects, e, hit.CritChance, hit.CritDamage);
                StartCoroutine(Survival(bounceCount, hit));
                Debug.LogError("Count= " + bounceCount);
            }
            else {
                this.Recycle();
            }
        }
        else
            this.Recycle();
    }
}
