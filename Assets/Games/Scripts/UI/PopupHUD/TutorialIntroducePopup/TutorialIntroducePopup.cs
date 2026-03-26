using DG.Tweening;
using GameSystem.Common.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialIntroducePopup : DOTweenFrame {
    [SerializeField] private ButtonBase tabToHideButton;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image iconKey;
    [SerializeField] private GameObject tabToHideText;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float timeScale = 1f;
    [SerializeField] private float timeShowTabToHide = 1.5f;
    private void Awake() {
        tabToHideButton?.AddEvent(OnFinishTutorialIntroduce);
    }
    public override Frame OnBack() {
        return this;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        ResetData();
        ShowContent();
    }
    private void ShowContent() {
        descriptionText.gameObject.SetActive(true);
        descriptionText.DOFade(1, timeScale).SetUpdate(true);
        descriptionText.transform.DOScale(Vector3.one, timeScale).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
            iconKey.gameObject.SetActive(true);
            if (effect != null)
                effect.Play();
            iconKey.transform.DOScale(Vector3.one, timeScale).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => {
                DOVirtual.DelayedCall(timeShowTabToHide, () => {
                    tabToHideText.SetActive(true);
                    tabToHideButton.interactable = true;
                }).SetUpdate(true);
            });
        });
    }
    private void ResetData() {
        tabToHideText.SetActive(false);
        iconKey.gameObject.SetActive(false);
        tabToHideButton.interactable = false;
        descriptionText.gameObject.SetActive(false);
        descriptionText.SetAlpha(0);
        iconKey.transform.localScale = Vector3.zero;
        descriptionText.transform.localScale = Vector3.zero;
    }

    private void OnFinishTutorialIntroduce() {
        tabToHideButton.interactable = false;
        GameResources.Instance.TutorialSytemData.SetFinishTutorialIntroduce(true)
                                                   .GetRewardKey()
                                                   .GetRewardEnergy();
        IngameHUD.Instance.GetCombat<ConquerorCombatPanel>().TutorialPlayState2();
        DOVirtual.DelayedCall(1f, () => SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadNormal));
        Hide();
        Time.timeScale = 1;
    }
}
