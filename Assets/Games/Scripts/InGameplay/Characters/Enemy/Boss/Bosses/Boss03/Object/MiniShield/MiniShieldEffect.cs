

using UnityEngine;

public class MiniShieldEffect : CharacterEffect {
    [SerializeField] protected EnemyHitEffect enemyHitEffect;

    public override void PreloadIngame() {
        if (enemyHitEffect) {
            enemyHitEffect.PreloadIngame();
        }
    }

    public virtual void StartEnemyHitEffect() {
        if (enemyHitEffect) {
            enemyHitEffect.StartEffect();
        }
    }
}
