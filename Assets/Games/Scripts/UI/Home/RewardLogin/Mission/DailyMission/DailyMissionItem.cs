using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionItem : MonoBehaviour, IItem<DailyMissionItemData> {
    [SerializeField] private Image icon;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI nameMission;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI rewardValue;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private ButtonExplorer gotoButton;
    [SerializeField] private ButtonExplorer cheatButton;
    [SerializeField] private GameObject tick;


    private Action onClaim;
    private Color normalColor;
    private Color highlightColor;
    public DailyMissionItemData dataStack { get; set; }

    private void Awake() {
        claimButton.AddEvent(Claim);
        cheatButton.AddEvent(Cheat);
        gotoButton.AddEvent(GotoSource);
#if !CHEAT
        cheatButton.gameObject.SetActive(false);
#endif
    }

    public IItem<DailyMissionItemData> Generate() {
        var claimable = dataStack.Claimable;
        var isComplete = dataStack.IsComplete;
        //icon.sprite = dataStack.Icon;
        nameMission.text = dataStack.NameMission;
        targetText.text = $"/{dataStack.PointTarget}";
        progressText.text = $"{dataStack.PointProgress}";
        rewardValue.text = $"{dataStack.Reward.Amount}";
        progressImage.fillAmount = dataStack.GetProgress();
        tick.SetActive(isComplete);
        SetColor(!claimable);
        claimButton.gameObject.SetActive(!dataStack.IsComplete && claimable);
        gotoButton.gameObject.SetActive(!dataStack.IsComplete && !claimable && dataStack.GotoSource != null);

        return this;
    }
    public DailyMissionItem SetNormalColor(Color normalColor) {
        this.normalColor = normalColor;
        return this;
    }
    public DailyMissionItem SetHighlighColor(Color highlightColor) {
        this.highlightColor = highlightColor;
        return this;
    }
    public void SetColor(bool isNormal) {
        progressImage.color = isNormal ? normalColor : highlightColor;
    }
    public DailyMissionItem UpdateUI(DailyMissionItemData data, Action onClaim) {
        dataStack = data;
        this.onClaim = onClaim;
        Generate();
        return this;
    }

    public void Claim() {
        dataStack.Apply();
        Generate();
        SetColor(true);
        onClaim?.Invoke();
    }
    private void GotoSource() {
        dataStack.GotoAction();
    }
    private void Cheat() {
        dataStack.SetProgress();
        Claim();
    }
}
