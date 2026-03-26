using Gemmob;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCurrencyItemView : CurrencyView {
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI txtClock;
    [SerializeField] private Image energyIcon;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private DotweenAnimation downEnergyAnim;
    private EnergyData energyData;
    private double currentCoundownValue;
    private TimeSpan timeSpan;
    private Countdowner cd = new Countdowner();
    private bool canCountdown;
    private void Awake() {
        energyData = GameResources.Instance.EnergyData;
        EventDispatcher.Instance.AddListener(EventKey.OnEnergyChanged, CheckCanCountdown);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnEnergyChanged, CheckCanCountdown);
    }
    private void OnDisable() {
        energyData.SaveQuitTime();
    }
    private void OnEnable() {
        currentCoundownValue = energyData.StartCountAt;
        canCountdown = energyData.CanCoundown();
        txtClock.gameObject.SetActive(canCountdown);
    }
    private void Update() {
        if (!canCountdown)
            return;
        if (cd.IsTimeOut()) {
            timeSpan = TimeSpan.FromSeconds(currentCoundownValue);
            if (timeSpan.TotalSeconds <= 0) {
                energyData.GetCoundownReward();
                currentCoundownValue = energyData.EnergyNeedToReload;
            }
            SetEnergyTimeContent(string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds), true);
            cd.StartCountdown(1);
            currentCoundownValue--;
            energyData.StartCountAt = (int)currentCoundownValue;
        }
        cd.Countdowning(Time.deltaTime);
    }
    private void OnApplicationPause(bool pause) {
        if (pause) {
            energyData.SaveQuitTime();
        }
        else {
            energyData.GiveEnergyOffline();
            canCountdown = energyData.CanCoundown();
            currentCoundownValue = energyData.StartCountAt;
        }
    }

    public override void Show() {
        if (Model == null) {
            return;
        }
        InitData();
        SetContentAmount(($"{Model.Amount}/{energyData.GetMaxEnergy()}"), true);
        PlayEffectUseEnergy();
    }
    private void InitData() {
        if (ItemIcon != null) {
            ItemIcon.sprite = Model.Icon;
        }
        if (energyData == null)
            energyData = GameResources.Instance.EnergyData;

    }
    private void PlayEffectUseEnergy() {
        int delta = Model.Amount - previousValue;
        if (delta < 0) {
            energyIcon.sprite = Model.Icon;
            energyText.text = delta.ToString();
            downEnergyAnim.Play();
        }
        previousValue = Model.Amount;

    }
    private void SetEnergyTimeContent(string content, bool show) {
        if (txtClock) {
            txtClock.gameObject.SetActive(show);
            if (show) {
                txtClock.text = content;
            }
        }
    }
    private void CheckCanCountdown() {
        canCountdown = energyData.CanCoundown();
        txtClock.gameObject.SetActive(canCountdown);
    }
}
