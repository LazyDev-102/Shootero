using System;
using TMPro;
using UnityEngine;

public class SmartOfferTime : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timeText;

    private Countdowner cd = new Countdowner();
    private TimeSpan timeSpan = new TimeSpan();
    private SmartOfferData data;

    private void Start() {
        cd.StartCountdown(1);
        data = GameResources.Instance.IapPack.SmartOffer;
    }
    private void Update() {
        if (cd.IsTimeOut()) {
            UpdateUI();
            cd.StartCountdown(1);
        }
        cd.Countdowning(Time.deltaTime);
    }
    private void UpdateUI() {
        timeSpan = TimeSpan.FromSeconds(data.GetTimeRemain());
        timeText.text = FormatTime();
    }

    private string FormatTime() {
        if (timeSpan.Seconds < 0)
            return "00:00";
        if (timeSpan.Hours > 0)
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }


}
