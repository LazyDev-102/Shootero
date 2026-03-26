using Gemmob;
using UnityEngine;
using DG.Tweening;
using Helper;

public class GameLoginEffect : MonoBehaviour {
    [SerializeField] private Transform effectContainer;
    [SerializeField] private Transform[] effects;

    private Countdowner effectCd = new Countdowner();
    public bool IsPlayEffect;

    public void PreloadEffect() {
        foreach (var item in effects) {
            item.RegisterPool(10);
        }
        IsPlayEffect = true;
    }

    private void Update() {
        if (IsPlayEffect) {
            if (effectCd.IsTimeOut()) {
                PlayEffect();
                effectCd.StartCountdown(.1f);
            }
            effectCd.Countdowning(Time.deltaTime);
        }
    }
    private void PlayEffect() {
        var newEffect = GetEffect().Spawn(effectContainer);
        newEffect.gameObject.SetActive(true);
        newEffect.localPosition = new Vector3(Random.Range(-540, 540), 2000, 0);
        newEffect.localScale = new Vector3(Random.Range(1f, 1.5f), Random.Range(1f, 2f), 1);

        var tweener = newEffect.DOLocalMove(new Vector3(newEffect.localPosition.x, -2000, 0), Random.Range(2f, 5f))
                               .SetEase(Ease.Linear)
                               .OnComplete(() => {
                                   newEffect.DOKill();
                                   newEffect.Recycle();
                               });
    }
    private Transform GetEffect() {
        return RandomHelper.RandomInCollection(effects);
    }
}