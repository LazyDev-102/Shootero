using DG.Tweening;
using Gemmob;
using UnityEngine;

public class B13HitEffect : EnemyHitEffect
{
    [SerializeField] private SpreadEnemyHitEffect iceSpreadhitEffect;
    [SerializeField] private Transform iceSpreadContainer;
    [SerializeField] private DOTweenAnimation iceWhiteEffect;
    public override void StartEffect(Sprite sprite = null) {
        base.StartEffect(sprite); 
        if (iceSpreadhitEffect) {
            SpreadEnemyHitEffect newEffect = iceSpreadhitEffect.Spawn(iceSpreadContainer);
            newEffect.StartSpread(sprite);
        }
        if (iceWhiteEffect != null) {
            iceWhiteEffect.gameObject.SetActive(true);
            iceWhiteEffect.DORestart();
        }
    }
}
