using DG.Tweening;
using Gemmob;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionLayout : MonoBehaviour, ILayout<DailyMissionItem, DailyMissionItemData> {
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI resetText;
    [SerializeField] private DailyMissionItem itemPrefab;
    [SerializeField] private DailyMissionProgressLayout dailyMissionProgress;
    [SerializeField] private ButtonExplorer claimAllButton;
    [SerializeField] private Transform[] effect;
    [SerializeField] private Transform coinTarget;
    [SerializeField] private ItemClaim itemReward;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private ScrollRect scroll;

    private DailyMissionData data;
    private DailyMissionItemData[] itemDatas;
    private bool isShowRemainText;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    public List<DailyMissionItem> Items { get; set; } = new List<DailyMissionItem>();

    private void Awake() {
        claimAllButton.AddEvent(OnClaimAll);
        isShowRemainText = true;
        data = GameResources.Instance.DailyMission;
        itemDatas = data.Datas;
    }

    public void UpdateUI() {
        ResetEffect();
        data.SetProgressing();
        dailyMissionProgress.UpdateUI(data.ProgressData, data.PointProgress, data.PointTarget);
        GenerateItem();
        SortMission();
        scroll.verticalNormalizedPosition = 1;
    }
    public void GenerateItem() {
        if (Items != null && Items.Count > itemDatas.Length) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < itemDatas.Length) {
                    Items[i].SetNormalColor(normalColor)
                            .SetHighlighColor(highlightColor)
                            .UpdateUI(itemDatas[i], OnClaim);
                }
                Items[i].gameObject.SetActive(i < itemDatas.Length);
            }
        }
        else {
            for (int i = 0; i < itemDatas.Length; i++) {
                if (Items == null || i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].SetNormalColor(normalColor)
                        .SetHighlighColor(highlightColor)
                        .UpdateUI(itemDatas[i], OnClaim);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            currentTime = DateTime.Now.TimeOfDay.TotalSeconds;
            timeRemain = Constant.DayToSecond - currentTime;
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            resetText.text = $"Reset in {timeSpan.Hours}h {timeSpan.Minutes}m";
            if (timeRemain <= 0) {
                isShowRemainText = false;
            }
            showTimeRemainTextCD.StartCountdown(1);
        }
        else {
            showTimeRemainTextCD.Countdowning(Time.deltaTime);
        }
    }
    private void Update() {
        if (isShowRemainText) {
            SetTimeRemain();
        }
    }
    private void OnClaim() {
        if (gameObject.activeInHierarchy) {
            SortMission();
            StartCoroutine(PlayEffect(Progress));
            PanelHUD.Instance.Conqueror.MissionPopupNotify();
        }
    }
    private void OnClaimAll() {
        foreach (var item in Items) {
            if (item != null && item.gameObject.activeInHierarchy && item.dataStack.Claimable) {
                item.Claim();
            }
        }
        claimAllButton.SetState(false);
    }
    private void SortMission() {
        bool isClaimAll = false;
        foreach (var item in Items) {
            if (item != null && item.gameObject.activeInHierarchy) {
                if (item.dataStack.IsComplete)
                    item.transform.SetAsLastSibling();
                if (item.dataStack.Claimable) {
                    isClaimAll = true;
                    item.transform.SetAsFirstSibling();
                }
            }
        }
        claimAllButton.SetState(isClaimAll);
    }
    private IEnumerator PlayEffect(Action onComplete) {
        if (effect == null || effect.Length == 0) {
            onComplete?.Invoke();
            yield break;
        }
        if (effect[0].gameObject.activeInHierarchy)
            yield break;
        for (int i = 0; i < effect.Length; i++) {
            var index = i;
            effect[index].localPosition = new Vector3(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-100f, -100f), 0);
            yield return Yielder.Wait(UnityEngine.Random.Range(0f, 0.3f));
            effect[index].gameObject.SetActive(true);
            effect[index].DOScale(UnityEngine.Random.Range(1f, 2f), 0.5f);
            effect[index].DOMove(coinTarget.position, 1.5f)
                     .SetEase(Ease.InExpo)
                     .OnComplete(() => {
                         effect[index].gameObject.SetActive(false);
                         effect[index].localPosition = Vector3.zero;
                     });
        }
        DOVirtual.DelayedCall(1.5f, () => onComplete?.Invoke());
    }
    private void ResetEffect() {
        for (int i = 0; i < effect.Length; i++) {
            effect[i].gameObject.SetActive(false);
            effect[i].localPosition = Vector3.zero;
        }
    }
    private void Progress() {
        //dailyMissionProgress.Progress(data.Progress);
        dailyMissionProgress.UpdateUI(data.ProgressData, data.PointProgress, data.PointTarget);
    }
}