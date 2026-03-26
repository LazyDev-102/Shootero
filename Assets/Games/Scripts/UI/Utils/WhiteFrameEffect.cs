using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFrameEffect : MonoBehaviour {
    [SerializeField] private Image frame;
    [SerializeField] private float delayTime;
    [SerializeField] private float duration;
    public void Show(System.Action onCompleted = null) {
        frame.transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        PlayEffect(delayTime, duration, onCompleted);
    }
    private void PlayEffect(float delayTime, float duration, System.Action onCompleted = null) {
        frame.SetAlpha(1);
        DOVirtual.DelayedCall(delayTime, () => {
            frame.DOFade(0, duration).SetUpdate(true);
            frame.transform.DOScale(Vector3.one * 1.2f, duration).OnComplete(() => onCompleted?.Invoke());
        }).SetUpdate(true);
    }
}
