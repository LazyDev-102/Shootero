using DG.Tweening;
using GameSystem.Common.UI;
using Helper;
using System;
using TMPro;
using UnityEngine;

public class BossCombatFrame : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private Transform timeOutTrans;
    [SerializeField] private GameObject timeOutGroup;

    private Countdowner timeLifeCd = new Countdowner();
    private BossModeInfo info;
    private TimeSpan timeSpan;
    private string m;
    private string s;
    private int duration;
    private bool isPlaying;
    private void OnEnable() {
        info = GameResources.Instance.BossModeData.GetInfo();
        UpdateUI();
        HUDManager.IgnoreUserInput(false);
    }
    public void Active(bool status) {
        info = GameResources.Instance.BossModeData.GetInfo();
        timeOutGroup.SetActive(status);
        if (status)
            OnStartGame();
    }
    public void OnStartGame() {
        duration = info.TimeLimit;
        isPlaying = true;
    }
    private void Update() {
        if (isPlaying && timeLifeCd.IsTimeOut()) {
            timeRemainText.text = FormatTime(duration);
            duration = (int)(duration - Time.timeScale);
            timeLifeCd.StartCountdown(1);
            if (duration < 0) {
                TimeOut();
                timeLifeCd.StartCountdown(1000);
            }
        }
        timeLifeCd.Countdowning(Time.deltaTime);
    }
    public void UpdateUI() {
        timeRemainText.text = FormatTime(duration);
    }
    public void TimeOut() {
        isPlaying = false;
        timeOutTrans.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuad);
        this.DelayWait(1f, () => {
            GameManager.Instance.Lose(true);
        });
    }
    private string FormatTime(int second) {
        timeSpan = TimeSpan.FromSeconds(second);
        m = timeSpan.Minutes < 10 ? $"0{timeSpan.Minutes}" : $"{timeSpan.Minutes}";
        s = timeSpan.Seconds < 10 ? $"0{timeSpan.Seconds}" : $"{timeSpan.Seconds}";
        return $"{m}:{s}";
    }
}
