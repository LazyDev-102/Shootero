using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HalloweenHeadPanel : DOTweenFrame {
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image processImage;
    [SerializeField] private Image bgProcessImage;

    private void OnEnable() {
	SetProgression();
        EventDispatcher.Instance.AddListener<EventKey.OnExpChange>(SetProgression);
        OnShow();
    }

    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnExpChange>(SetProgression);
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        SetProgression();
    }

    private void SetProgression() {
        if (!gameObject.activeInHierarchy)
            return;
        var currentExp = GameResources.Instance.LevelProgress.Datas.OwnedExp;
        var maxExp = GameResources.Instance.LevelProgress.Datas.GetMaxExpInLevel();
        var ratio = Convert.ToSingle(currentExp) / Convert.ToSingle(maxExp);
        if (maxExp == -1) {
            processImage.fillAmount = 1;
        }
        else {
            StartCoroutine(Process(ratio));
        }
        levelText.text = $"{GameResources.Instance.LevelProgress.Datas.CurrentLv + 1}";
    }
    private IEnumerator Process(float ratio) {
        var timeUsing = 0f;
        while (timeUsing <= 0.5f) {
            timeUsing += Time.deltaTime;
            processImage.fillAmount = ratio * timeUsing * 2;
            yield return null;
        }
    }
}
