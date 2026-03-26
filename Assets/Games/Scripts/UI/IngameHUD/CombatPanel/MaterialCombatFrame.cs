using DG.Tweening;
using GameSystem.Common.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCombatFrame : MonoBehaviour {
    [SerializeField] private ModesBuffEffect buffEffect;
    [SerializeField] private float startFade;
    [SerializeField] private float endFade;
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Transform timeOutTrans;
    [SerializeField] private GameObject timeOutGroup;
    [SerializeField] private GameObject rewardGroup;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Sprite specialIcon;
    [SerializeField] private RewardItem[] reward;
    [SerializeField] private Transform rewardContainer;

    private Countdowner timeLifeCd = new Countdowner();
    private MaterialModeInfo info;
    private MaterialModeInfo.MaterialModeRewardInfo rewardInfo;
    private TimeSpan timeSpan;
    private string m;
    private string s;
    private int duration;
    private int date;
    private bool isPlaying;
    private void OnEnable() {
        date = (int)DateTime.Now.DayOfWeek;
        info = GameResources.Instance.MaterialModeData.GetInfo();
        UpdateUI();
        HUDManager.IgnoreUserInput(false);
    }
    public void Active(bool status) {
        date = (int)DateTime.Now.DayOfWeek;
        info = GameResources.Instance.MaterialModeData.GetInfo();
        rewardInfo = info.Reward[date];
        timeOutGroup.SetActive(status);
        rewardGroup.SetActive(status && date < 6);
        if (status)
            OnStartGame();
    }
    public void OnStartGame() {
        duration = info.TimeLimit;
        isPlaying = true;
    }
    public void SetMaxWave() {
        IngameHUD.Instance.Combat.SetMaxWave(info.MaxWave);
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
        rewardIcon.sprite = date == 0 ? specialIcon : info.Reward[date].RewardPerWave[0].Icon;
        rewardText.text = "0";
    }
    public void ChangeReward(int waveIndex) {
        rewardText.text = date == 0 ? $"{waveIndex * info.Reward[date].RewardPerWave[2].Amount * 5}" : $"{waveIndex * info.Reward[date].RewardPerWave[0].Amount}";
    }
    public void TimeOut() {
        isPlaying = false;
        timeOutTrans.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuad);
        GameManager.Instance.Lose(true);
    }
    private string FormatTime(int second) {
        timeSpan = TimeSpan.FromSeconds(second);
        m = timeSpan.Minutes < 10 ? $"0{timeSpan.Minutes}" : $"{timeSpan.Minutes}";
        s = timeSpan.Seconds < 10 ? $"0{timeSpan.Seconds}" : $"{timeSpan.Seconds}";
        return $"{m}:{s}";
    }

    public void ShowRewardOnWinWave() {
        int countActive = rewardInfo.RewardPerWave.Length;
        for (int i = 0; i < reward.Length; i++) {
            if (i < countActive)
                reward[i].SetIcon(rewardInfo.RewardPerWave[i].Icon, true)
                         .SetAmountText(rewardInfo.RewardPerWave[i].Amount.ToString(), true)
                         .gameObject.SetActive(true);
            else
                reward[i].gameObject.SetActive(false);
        }
        rewardContainer.gameObject.SetActive(true);
        rewardContainer.DOScale(Vector3.one, .3f).OnComplete(() => {
            DOVirtual.DelayedCall(2f, () => {
                rewardContainer.DOScale(Vector3.zero, 1f);
            });
        });
    }
    public void PlayModesBuffEffect(bool isBuff, string description) {
        buffEffect.gameObject.SetActive(true);
        buffEffect.BuffFrame.gameObject.SetActive(true);
        buffEffect.ShowFade(isBuff, description, startFade, endFade);
    }
    public void StopModesBuffEffect() {
        buffEffect.StopShowFadeConfig();
        buffEffect.BuffFrame.gameObject.SetActive(false);
    }
    public void PlayImmortalEffect(bool isBuff, string description) {
        buffEffect.gameObject.SetActive(true);
        buffEffect.BuffFrame.gameObject.SetActive(true);
        buffEffect.ShowFade(isBuff, description, startFade, endFade);
    }
    public void StopImmortalEffect() {
        buffEffect.StopShowFadeConfig();
        buffEffect.BuffFrame.gameObject.SetActive(false);
    }
}
