using UnityEngine;

public class EnemyEffect : CharacterEffect {
    [Header("EnemyEffect")]
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
