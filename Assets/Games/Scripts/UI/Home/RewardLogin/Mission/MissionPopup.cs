using GameSystem.Common.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionPopup : DOTweenFrame {
    [SerializeField] private DailyMissionLayout dailyMission;
    [SerializeField] private ChallengeLayout challengeLayout;
    [SerializeField] private ButtonExplorer dailyMissionButton;
    [SerializeField] private ButtonExplorer challengeButton;
    [SerializeField] private TextMeshProUGUI dailyMissionText;
    [SerializeField] private TextMeshProUGUI challengeText;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Color selectColor;
    [SerializeField] private Color unselectColor;

    public DailyMissionLayout DailyMission { get => dailyMission; }
    public ChallengeLayout Challenge { get => challengeLayout; }

    private void Awake() {
        closeButton.AddEvent(OnClose);
        dailyMissionButton.AddEvent(() => OpenPage(true));
        challengeButton.AddEvent(() => OpenPage(false));
    }
    public void OpenPage(bool isDailyMission) {
        OpenDailyMission(isDailyMission);
        OpenChallenge(!isDailyMission);
        SetButtonStatus(isDailyMission);
    }
    private void SetButtonStatus(bool isDaily) {
        dailyMissionButton.SetColor(isDaily ? selectColor : unselectColor);
        challengeButton.SetColor(!isDaily ? selectColor : unselectColor);
        dailyMissionText.SetAlpha(isDaily ? 1 : 0.5f);
        challengeText.SetAlpha(!isDaily ? 1 : 0.5f);
    }
    private void OpenDailyMission(bool status) {
        dailyMission.gameObject.SetActive(status);
        if (!status)
            return;
        dailyMission.UpdateUI();
        title.text = GameResources.Instance.DailyMission.NameEvent;
    }
    private void OpenChallenge(bool status) {
        challengeLayout.gameObject.SetActive(status);
        if (!status)
            return;
        challengeLayout.Initialize();
        title.text = GameResources.Instance.Challenge.NameEvent;
    }
    private void OnClose() {
        Hide();
        PanelHUD.Instance.Conqueror.MissionPopupNotify();
    }
    public override Frame OnBack() {
        PanelHUD.Instance.Conqueror.MissionPopupNotify();
        return base.OnBack();
    }
}
