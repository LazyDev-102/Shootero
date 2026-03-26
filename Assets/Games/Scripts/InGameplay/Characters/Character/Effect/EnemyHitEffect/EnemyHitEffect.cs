

using DG.Tweening;
using Gemmob;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour {
    [SerializeField] private SpreadEnemyHitEffect spreadHitEffect;
    [SerializeField] private Transform spreadContainer;

    [SerializeField] private DOTweenAnimation whiteEffect;
    [SerializeField] private int numberPreload;

    public virtual void PreloadIngame() {
        if (spreadHitEffect) {
            spreadHitEffect.RegisterPool(numberPreload);
        }
    }

    public virtual void StartEffect(Sprite sprite = null) {
        if (spreadHitEffect) {
            SpreadEnemyHitEffect newEffect = spreadHitEffect.Spawn(spreadContainer);
            newEffect.StartSpread(sprite);
        }
        if (whiteEffect != null) {
            whiteEffect.gameObject.SetActive(true);
            whiteEffect.DORestart();
        }
    }
}
