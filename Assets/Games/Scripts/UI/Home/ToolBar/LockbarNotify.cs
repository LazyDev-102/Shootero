using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockbarNotify : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI content;
    [SerializeField] private TMPro.TextMeshProUGUI iconContent;
    [SerializeField] private GameObject icon;
    [SerializeField] private UnityEngine.UI.Image background;

    [SerializeField] private float distanceMove = 2f;

    [SerializeField] private float time = 1f;
    [SerializeField] private float timeOut = 0.5f;
    private Vector2 origin;
    TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> moveTweener1;
    TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> moveTweener2;
    Tween delay;

    private void Start() {
        origin = transform.position;
    }
    public LockbarNotify SetOriginPos(Vector2 pos) {
        origin = pos;
        return this;
    }
    public LockbarNotify SetContent(string text, float timeMove, bool showWithIcon = false, string iconContent = "") {
        content.text = text;
        time = timeMove;
        icon.SetActive(showWithIcon);
        content.gameObject.SetActive(!showWithIcon);
        this.iconContent.text = iconContent;
        return this;
    }


    public LockbarNotify Show() {
        ResetState();
        gameObject.SetActive(true);
        StartCoroutine(SetAlpha(content, time, true));
        StartCoroutine(SetAlpha(background, time, true));
        moveTweener1 = transform.DOMoveY(origin.y + distanceMove, time).SetUpdate(true).OnComplete(() => {
            delay = DOVirtual.DelayedCall(0.5f, () => {
                if (gameObject.activeInHierarchy) {
                    moveTweener2 = transform.DOMoveY(transform.position.y + distanceMove / 2, timeOut).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
                    StartCoroutine(SetAlpha(content, timeOut - 0.1f, false));
                    StartCoroutine(SetAlpha(background, timeOut - 0.1f, false));
                }
            }).SetUpdate(true);
        });
        return this;
    }

    IEnumerator SetAlpha(UnityEngine.UI.Graphic graphic, float time, bool increase, System.Action onComplete = null) {
        var duration = 0f;
        if (increase) {
            graphic.SetAlpha(0);
            while (duration < time) {
                duration += 0.02f;
                graphic.SetAlpha(duration / time);
                yield return new WaitForSecondsRealtime(0.02f);
            }
            graphic.SetAlpha(1);
        }
        else {
            graphic.SetAlpha(1);
            while (duration < time) {
                duration += 0.02f;
                graphic.SetAlpha(1 - duration / time);
                yield return new WaitForSecondsRealtime(0.02f);
            }
            graphic.SetAlpha(0);
        }
        onComplete?.Invoke();
    }

    private void ResetState() {
        StopAllCoroutines();
        if (moveTweener1 != null)
            moveTweener1.Kill(false);
        if (moveTweener2 != null)
            moveTweener2.Kill(false);
        if (delay != null)
            delay.Kill(false);
        gameObject.SetActive(false);
        content.SetAlpha(0);
        background.SetAlpha(0);
        transform.position = origin;
    }
}
