using UnityEngine;
public class ChestEffect : CharacterEffect
{
    [SerializeField] protected EnemyHitEffect chestHitEffect;

    public override void PreloadIngame() {
        if (chestHitEffect) {
            chestHitEffect.PreloadIngame();
        }
    }

    public virtual void StartChestHitEffect() {
        if (chestHitEffect) {
            chestHitEffect.StartEffect();
        }
    }
}
