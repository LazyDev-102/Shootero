using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressFillAmountBase : MonoBehaviour {
    [SerializeField] protected Image processImage;
    [SerializeField] protected TextMeshProUGUI processText;
    [SerializeField] protected float speed = 1f;

    protected float maxWidth;
    protected float distace;
    protected bool isComplete;
    protected Action onComplete;
    protected float originSpeed;
    protected GameObject goCache;
    private void Awake() {
        Assign();
    }

    protected virtual void Assign() {
        originSpeed = speed;
        goCache = gameObject;
    }

    public void FillAmountBar(Image img, float fillAmount) {
        if (fillAmount > 1)
            fillAmount = 1;
        img.fillAmount = fillAmount;
        if (processText)
            processText.text = $"{(int)(fillAmount * 100)}%";
    }
    public void FillBar(float startPct, float endPct) {
        ForceFillAmountBar(startPct);
        HandleBarChanged(endPct);
    }
    public void FillBar(float endPct) {
        HandleBarChanged(endPct);
    }

    public virtual void HandleBarChanged(float pct) {
        if (GameManager.Initialized && GameManager.Instance.isTest)
            return;
        if (ReferenceEquals(goCache, null) || gameObject == null || !gameObject.activeInHierarchy) {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(ChangingAmountBar(pct));
    }

    protected virtual IEnumerator ChangingAmountBar(float pct) {
        isComplete = false;
        float elapsed = processImage.fillAmount;
        float delta = speed * Time.fixedDeltaTime;
        bool increase = elapsed < pct;
        while (Mathf.Abs(elapsed - pct) > delta) {
            elapsed = increase ? elapsed + delta : elapsed - delta;
            FillAmountBar(processImage, elapsed);
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }

        FillAmountBar(processImage, pct);
        Completed();
    }

    protected virtual void Completed() {
        isComplete = true;
        if (onComplete != null) {
            Action onAction = onComplete;
            onComplete = null;
            onAction.Invoke();
        }
    }

    public void ForceFillAmountBar(float pct) {
        FillAmountBar(processImage, pct);

    }

    public void AddOnComplete(Action onComplete) {
        this.onComplete = onComplete;
    }

    public void RemoveOnComplete() {
        this.onComplete = null;
    }

    [ContextMenu("Down")]
    private void ProcessDown() {
        var start = processImage.fillAmount <= 0 ? 1 : processImage.fillAmount;
        var end = start + UnityEngine.Random.Range(-0.5f, -0.1f);
        FillBar(start, end);
    }
    [ContextMenu("Up")]
    private void ProcessUp() {
        var start = processImage.fillAmount >= 1 ? 0 : processImage.fillAmount;
        var end = start + UnityEngine.Random.Range(0.1f, 0.5f);
        FillBar(start, end);
    }
}
