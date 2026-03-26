using DG.Tweening;
using Gear_Data;
using Gemmob;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearDetailItemStatsPrimary : MonoBehaviour, IItem<LevelStatData> {
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI valueIncreaseText;
    [SerializeField] private GameObject arrow;
    [SerializeField] Image background;
    [SerializeField] float timeScaleText = 0.2f;
    public LevelStatData dataStack { get; set; }
    private int currentLevel;
    private bool isUpgradeSuccess;
    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnEnhanceGear>(OnEnhanceGear);
    }
    private void OnDestroy() {

        EventDispatcher.Instance.RemoveListener<EventKey.OnEnhanceGear>(OnEnhanceGear);
    }
    public IItem<LevelStatData> Generate() {
        var maxLv = currentLevel + 1 == dataStack.Values.Length ? true : false;
        mainText.text = dataStack.StatData.GetDescription(dataStack.Values[currentLevel - 1].Value);
        if (!maxLv && !isUpgradeSuccess) {
            if (arrow != null)
                arrow.SetActive(true);
            valueIncreaseText.gameObject.SetActive(true);
            //valueIncreaseText.text = hasClose ? GetIncreaseValue(data.Values[currentLevel + 1].Type) : GetIncreaseValue(data.Values[currentLevel + 1].Type);
            valueIncreaseText.text = $" {dataStack.StatData.GetValueString(dataStack.Values[currentLevel].Value)})";
        }
        else {
            if (arrow != null)
                arrow.SetActive(false);
            valueIncreaseText.gameObject.SetActive(false);

        }
        return this;
    }

    public void Initialized(LevelStatData data, int currentLevel, bool isUpgradeSuccess = true) {
        this.dataStack = data;
        this.currentLevel = currentLevel;
        this.isUpgradeSuccess = isUpgradeSuccess;
        Generate();
    }

    private string GetIncreaseValue(StatModType type) {
        switch (type) {
            case StatModType.PercentAdd:
                return $"{dataStack.Values[currentLevel + 1].Value * 100}%)";
        }
        return $"{dataStack.Values[currentLevel + 1].Value})";

    }
    public void ActiceBackground(bool active) {
        if (background != null)
            background.enabled = active;
    }
    private void OnEnhanceGear(EventKey.OnEnhanceGear data) {
        valueIncreaseText.transform.DOKill(true);
        valueIncreaseText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            valueIncreaseText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
        mainText.transform.DOKill(true);
        mainText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            mainText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
    }
}
