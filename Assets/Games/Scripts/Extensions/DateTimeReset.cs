using System;
using TMPro;
using UnityEngine;

public class DateTimeReset : MonoBehaviour {
    [SerializeField] GameAction onCompleted;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] string preDes;

    private Countdowner timeCd = new Countdowner();
    private bool active;
    private double fullTime;
    private TimeSpan timeSpan;

    private void OnEnable() {
        ResetData();
        active = true;
    }
    private void OnDisable() {
        active = false;
    }
    public void ResetData() {
        fullTime = Constant.DayToSecond - DateTime.Now.TimeOfDay.TotalSeconds + 1;
        timeCd.StartCountdown(1);
    }
    private void Update() {
        if (active) {
            timeCd.Countdowning();
            if (timeCd.IsTimeOut()) {
                if (fullTime > 0) {
                    fullTime--;
                    timeSpan = TimeSpan.FromSeconds(fullTime);
                    timeText.text = preDes + FormatTime();
                    timeCd.StartCountdown(1);
                }
                else {
                    ResetData();
                    onCompleted?.Execute();
                }
            }
        }
    }

    private string FormatTime() {
        if (timeSpan.Seconds < 0)
            return " 00:00";
        if (timeSpan.Hours > 0)
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

#if UNITY_EDITOR
    [ContextMenu("Show Second")]
    void ShowSecond() {
        Debug.LogError(DateTime.Now.TimeOfDay.TotalSeconds);
    }
#endif
}
