
using Gemmob;
using UnityEngine;

public class MiniLighiningCircle : BulletBase {
    [SerializeField] private float rotateSpeed;

    private void Update() {
        if (sprite) {
            sprite.transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
        }
    }

    public override void DestroyWithEffect() {
        if (explosion != null) {
            GameManager.Instance.GameLoader.SpawnEffectExplosion(explosion, transform.position);
        }
        onDestroy?.Invoke(transform.position);
        gameObject.SetActive(false);
    }

    protected override void Destroy() {
        onDestroy?.Invoke(transform.position);
        gameObject.SetActive(false);
    }
}
