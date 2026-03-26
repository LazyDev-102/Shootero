using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class ModItemDisplayer : MonoBehaviour {
    [SerializeField] private Image imgIcon;
    [SerializeField] private Image endIcon;
    [SerializeField] private VerticalLayoutGroup layoutIcon;
    [SerializeField] private Image imgFrame;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private Transform scaleContainer;
    [SerializeField] private ButtonBase btnChoose;
    [SerializeField] private ParticleSystem flareTextEffect;
    [SerializeField] private Image selectEffect;
    [SerializeField] private Image[] listModIcon;
    [SerializeField] private GameObject frameEffect;
    [SerializeField] private int offset = 420;
    public Image Icon { get => imgIcon; }
    public TextMeshProUGUI Name { get => txtName; }
    private Action onItemClicked;

    private void Start() {
        btnChoose.AddEvent(OnChooseButtonClicked);
    }
    private void OnEnable() {
        HideInfo();
        RandomIcon();
        CanClick(false);
    }
    public ModItemDisplayer SetIcon(Sprite sprite) {
        imgIcon.sprite = sprite;
        return this;
    }

    public ModItemDisplayer SetName(string name) {
        txtName.text = name;
        return this;
    }
    public ModItemDisplayer SetAlpha(float value) {
        txtName.SetAlpha(value);
        imgIcon.SetAlpha(value);
        return this;
    }
    public ModItemDisplayer OnItemClicked(Action onClick) {
        onItemClicked = onClick;
        return this;
    }
    public ModItemDisplayer SetAlphaAll(float time, Action onComplete = null) {
        float timeUse = 0f;
        txtName.gameObject.SetActive(true);
        transform.DOScaleX(1, time).SetUpdate(true).OnUpdate(() => {
            timeUse += Time.deltaTime;
            txtName.SetAlpha(timeUse / time);
            imgFrame.SetAlpha(timeUse / time);
            imgIcon.SetAlpha(timeUse / time);
        }).OnComplete(() => {
            txtName.SetAlpha(1);
            imgFrame.SetAlpha(1);
            imgIcon.SetAlpha(1);
            onComplete?.Invoke();
        });
        return this;
    }

    private void OnChooseButtonClicked() {
        onItemClicked?.Invoke();
        SetFrameEffectStatus(false);
    }
    public void ChooseCheat() {
        OnChooseButtonClicked();
    }
    public void CanClick(bool state) {
        btnChoose.interactable = state;
    }

    public void Hiding() {
        scaleContainer.DOScale(Vector3.zero, 0.3f).SetUpdate(true).SetEase(Ease.InOutBack);
    }
    private void HideInfo() {
        layoutIcon?.gameObject.SetActive(false);
        txtName.gameObject.SetActive(false);
        SetFrameEffectStatus(false);
    }
    private void RandomIcon() {
        for (int i = 0; i < listModIcon.Length; i++) {
            listModIcon[i].sprite = GameResources.Instance.ModGenerator.GetRandomModIcon();
        }
    }
    private void ShowInfo() {
        txtName.transform.localScale = Vector3.zero;
        txtName.gameObject.SetActive(true);
        txtName.transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        txtName.DOFade(1, 0.5f).SetUpdate(true);
        if (flareTextEffect) {
            flareTextEffect.time = 0;
            flareTextEffect.Play();
        }
        if (selectEffect) {
            selectEffect.gameObject.SetActive(true);
            selectEffect.DOFade(0, 0.5f).SetUpdate(true);
            selectEffect.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetUpdate(true).OnComplete(() => {
                selectEffect.gameObject.SetActive(false);
                selectEffect.transform.localScale = Vector3.one;
                selectEffect.SetAlpha(1);
            });
        }
    }
    public IEnumerator Showing(int endValue, Sprite spriteSelect, int speed = 70, Action onComplete = null) {
        if (layoutIcon == null)
            yield break;

        HideInfo();
        float deltaShowTime = 1.0f * endValue / speed;
        Countdowner delayPlaySFX = new Countdowner();
        delayPlaySFX.StartCountdown(deltaShowTime / 2);
        imgIcon.gameObject.SetActive(false);
        endIcon.sprite = spriteSelect;

        layoutIcon.padding.top = 24;
        layoutIcon.SetLayoutVertical();
        layoutIcon.gameObject.SetActive(true);


        while (layoutIcon.padding.top > endValue + speed) {
            if (delayPlaySFX.IsTimeOut()) {
                SoundManager.Instance.PlayRandomMod();
                delayPlaySFX.StartCountdown(deltaShowTime);
            }
            delayPlaySFX.Countdowning(0.02f);
            layoutIcon.padding.top -= speed;
            layoutIcon.SetLayoutVertical();
            yield return new WaitForSecondsRealtime(0.02f);
        }

        layoutIcon.padding.top = endValue;
        layoutIcon.SetLayoutVertical();
        onComplete?.Invoke();
        SetIcon(spriteSelect);
        imgIcon.gameObject.SetActive(true);
        SoundManager.Instance.PlayChooseMod();
        ShowInfo();
    }
    public void ResetUI() {
        HideInfo();
    }
    #region Tutorial
    public void OffsetLayoutTutorial() {
        layoutIcon.padding.top += offset;
        layoutIcon.SetLayoutVertical();
        SetFrameEffectStatus(true);
    }
    private void SetFrameEffectStatus(bool status) {
        if (frameEffect != null)
            frameEffect.SetActive(status);
    }
    #endregion
}
