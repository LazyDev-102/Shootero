
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Gemmob;
using GameSystem.Common.UI;
using System;

public class PlayerLevelBar : ProgressFillAmountBase {
    [SerializeField] private float zoomInValue = 0.3f;
    [SerializeField] private float zoomOutValue = 1f;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showAnimation;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideAnimation;
    [SerializeField] private Image expVirtual;
    [SerializeField] private ParticleSystem focusEffect;
    [SerializeField] private ParticleSystem decreaseLevelupEffect;
    [SerializeField] private TMPro.TextMeshProUGUI numberLevelupText;
    [SerializeField] private Image numberLevelupGO;
    [SerializeField] private Canvas levelBarCanvas;
    [SerializeField] private Transform target;

    private float oldPct;
    private ShipBase ship;
    private Tween tween;
    private float offsetExpVirtualHigh = 50;
    private float offsetExpVirtualTime = 0.3f;
    private int effectExpUpStatus = 0;
    private float timeDelayEffect = 0;
    private int numberLevelup;
    private float expMaxSize;
    public bool CanChooseMod { get => numberLevelup > 0; }
    //private void OnEnable() {
    //    transform.DOMove(target.position, 1f);
    //}
    private void Start() {
        transform.DOMove(target.position, 1f);
        expMaxSize = IngameHUD.Instance.transform.parent.rectTransform().sizeDelta.x + transform.rectTransform().sizeDelta.x;
    }
    private void Update() {
        if (timeDelayEffect > 0) {
            timeDelayEffect -= Time.deltaTime;
        }
    }
    public void AddListeners(ShipBase player) {
        ship = player;
        player.ShipLevel.AddOnPercentExpChanged(HandlePlayerHealthChanged);
        player.ShipLevel.AddOnLeveling(OnStartLeveling);
    }

    public void RemoveListenerEXPChanged(ShipBase player) {
        player.ShipLevel.AddOnPercentExpChanged(HandlePlayerHealthChanged);
    }
    private void HandlePlayerHealthChanged(float pct) {
        HandleBarChanged(pct);
        ZoomOut();
    }
    public override void HandleBarChanged(float pct) {
        if (!gameObject.activeInHierarchy) {
            return;
        }
        //StopAllCoroutines();
        ChoosePlayEffect(pct);
    }
    private void ChoosePlayEffect(float pct) {
        effectExpUpStatus++;
        switch (effectExpUpStatus) {
            case 1:
                IncreaseExpEffect(pct);
                oldPct = pct;
                break;
            default:
                timeDelayEffect += offsetExpVirtualTime;
                DOVirtual.DelayedCall(timeDelayEffect, () => {
                    IncreaseExpEffect(pct);
                    oldPct = pct;
                });
                break;
        }
    }
    private void IncreaseExpEffect(float pct) {
        var exp = expVirtual.Spawn(expVirtual.transform.parent, Vector3.zero);
        var expRect = exp.rectTransform;
        expRect.anchoredPosition = new Vector2(processImage.fillAmount * expMaxSize, offsetExpVirtualHigh);
        expRect.sizeDelta = new Vector2((pct - oldPct) * expMaxSize, expVirtual.rectTransform.sizeDelta.y);
        exp.gameObject.SetActive(true);
        exp.SetAlpha(0);
        exp.DOFade(1, offsetExpVirtualTime);
        expRect.DOAnchorPosY(expRect.anchoredPosition.y - offsetExpVirtualHigh, offsetExpVirtualTime).OnComplete(() => {
            if (pct == 0) {
                FillAmountBar(processImage, 1);
            }
            Completed();
            FillAmountBar(processImage, pct);
            exp.Recycle();
            effectExpUpStatus--;
        });
    }
    //private void IncreaseExpEffect(float pct) {
    //    var width = (pct - oldPct) * maxWidth;
    //    var exp = expVirtual.Spawn(imgCurrentValueReal.transform.parent, imgCurrentValueReal.transform.position + Vector3.up * offsetExpVirtualHigh);
    //    var temp = exp.transform.localPosition;
    //    temp.x = imgCurrentValueReal.rectTransform.anchoredPosition.x + imgCurrentValueReal.rectTransform.sizeDelta.x;
    //    if (pct - oldPct < 0) {
    //        width = pct * maxWidth;
    //        temp.x = imgCurrentValueReal.rectTransform.anchoredPosition.x;
    //    }
    //    exp.transform.localPosition = temp;
    //    exp.gameObject.SetActive(true);
    //    exp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    //    exp.SetAlpha(0);
    //    exp.DOFade(1, offsetExpVirtualTime);
    //    exp.transform.DOMoveY(exp.transform.position.y - offsetExpVirtualHigh, offsetExpVirtualTime).OnComplete(() => {
    //        if (pct == 0) {
    //            FillBar(imgCurrentValueReal, 1);
    //        }
    //        Completed();
    //        FillBar(imgCurrentValueReal, pct);
    //        exp.Recycle();
    //        effectExpUpStatus--;
    //    });
    //}

    protected override void Completed() {
        base.Completed();
        tween = DOVirtual.DelayedCall(1.5f, ZoomIn);
    }

    private void OnStartLeveling() {
        ZoomOut();
        if (gameObject.activeInHierarchy) {
            onComplete = () => {
                ship.ShipLevel.LevelUp();
                ForceFillAmountBar(0);
            };
        }
        else {
            ship.ShipLevel.LevelUp();
            ForceFillAmountBar(0);
        }

        HandleBarChanged(1f);
    }
    private void ZoomIn() {
        processImage.SetAlpha(0.3f);
    }
    private void ZoomOut() {
        transform.DOKill();
        tween?.Kill();
        processImage.SetAlpha(1);
    }

    public void Show(Action onComplete) {
        if (showAnimation) {
            showAnimation.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    public void Hide(Action onComplete) {
        if (hideAnimation) {
            hideAnimation.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }
    public void SetNumberLevelUp(int value = 1, Action onComplete = null) {
        numberLevelup += value;
        if (numberLevelup < 1) {
            numberLevelupText.SetAlpha(0);
            numberLevelupText.text = $"{numberLevelup}";
            numberLevelupText.DOFade(0, 1f).SetEase(Ease.Linear).SetUpdate(true);
            numberLevelupGO.DOFade(0, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
                numberLevelupGO.SetAlpha(1);
                numberLevelupGO.gameObject.SetActive(false);
            });
            return;
        }
        numberLevelupGO.gameObject.SetActive(true);
        //numberLevelupGO.SetAlpha(0);
        //numberLevelupGO.DOFade(1, 1f);
        numberLevelupText.SetAlpha(0);
        numberLevelupText.text = $"{numberLevelup}";
        numberLevelupText.DOFade(1, 1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => onComplete?.Invoke());
        if (gameObject.activeInHierarchy)
            StartCoroutine(PlayNumberLeverUpEffect(1f));
    }
    private IEnumerator PlayNumberLeverUpEffect(float duration) {
        if (decreaseLevelupEffect != null) {
            decreaseLevelupEffect.gameObject.SetActive(true);
            decreaseLevelupEffect.Play();
            yield return Yielder.Wait(duration);
            decreaseLevelupEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            yield return Yielder.Wait(1f);
            decreaseLevelupEffect.gameObject.SetActive(false);
        }
    }
    public void PlayFocusLevelEffect() {
        if (focusEffect != null) {
            focusEffect.gameObject.SetActive(true);
            focusEffect.Play();
            //yield return new WaitForSecondsRealtime(focusEffect.main.duration + 0.5f);
            //Yielder.Wait(focusEffect.main.duration + 0.5f);
            //focusEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            //focusEffect.gameObject.SetActive(false);
        }
    }
    public PlayerLevelBar SetCanvasStatus(bool status) {
        levelBarCanvas.enabled = status;
        return this;
    }
}
