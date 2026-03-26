

using UnityEngine;

public class ExtendHitbox : MonoBehaviour, IHitbox {
    [SerializeField] private CharacterHitbox characterHitbox;
    [SerializeField] protected EnemyHitEffect enemyHitEffect;

    public void PreloadIngame() {
        if (enemyHitEffect) {
            enemyHitEffect.PreloadIngame();
        }
    }

    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        characterHitbox.TakeHit(hit, positionCollider, type);
        if (!characterHitbox.CharacterBase.IsDie() && !characterHitbox.IsInvulnerable) {
            if (enemyHitEffect) {
                enemyHitEffect.StartEffect();
            }
        }
    }

    public Transform Transform() {
        return characterHitbox.Transform();
    }
}
