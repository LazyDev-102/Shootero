

using DG.Tweening;
using Gemmob;
using UnityEngine;

public class SpreadEnemyHitEffect : MonoBehaviour {
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeStartValue;
    [SerializeField] private float fadeEndValue;
    [SerializeField] private Ease fadeEase;

    [SerializeField] private float scaleDuration;
    [SerializeField] private Vector3 scaleStartValue;
    [SerializeField] private Vector3 scaleEndValue;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private bool isAutoDisable = true;


    private Tweener fadeTweener;
    private Tweener scaleTweener;

    public void StartSpread(Sprite sprite) {
        if (sprite != null) {
            this.sprite.sprite = sprite;
        }
        Reload();
        fadeTweener = this.sprite.DOFade(fadeEndValue, fadeDuration).SetEase(fadeEase);
        scaleTweener = this.sprite.transform.DOScale(scaleEndValue, scaleDuration).SetEase(scaleEase).OnComplete(OnComplete);
    }

    private void OnComplete() {
        if (isAutoDisable) {
            gameObject.Recycle();
        }
    }

    private void Reload() {
        var temp = sprite.color;
        temp.a = fadeStartValue;
        sprite.color = temp;
        sprite.transform.localScale = scaleStartValue;


        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

    }
}
