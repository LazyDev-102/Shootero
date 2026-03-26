
using UnityEngine;

public class MiniLightningLine : BulletBase {
    [SerializeField] private LightningLineBolt lineBolt;

    protected override void Hit(Collider2D collision) {
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(HitInfor, transform.position);
        }
        gameObject.SetActive(false);
    }

    protected override void Destroy() {
        onDestroy?.Invoke(transform.position);
    }

    public override void DestroyWithEffect() {
        if (explosion != null) {
            GameManager.Instance.GameLoader.SpawnEffectExplosion(explosion, transform.position);
        }
        onDestroy?.Invoke(transform.position);
    }

    private void Update() {
        lineBolt.Draw();
    }
}
