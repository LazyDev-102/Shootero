

using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InfinityPanel : DOTweenFrame {
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private ButtonExplorer changeNameButton;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private InfinityInputInfo inputInfo;
    [SerializeField] private ButtonBase btnPlay;
    [SerializeField] private ItemView energyPlayView;

    [SerializeField] private DOTweenAnimation showLeftToRight;
    [SerializeField] private DOTweenAnimation showRightToLeft;
    [SerializeField] private DOTweenAnimation hideLeftToRight;
    [SerializeField] private DOTweenAnimation hideRightToLeft;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject boardNoInternet;
    [SerializeField] private GameObject boardSignin;
    [SerializeField] private ButtonExplorer signinGpsButton;
    [SerializeField] private List<InfinityPlayerInfo> playerInfos;
    [SerializeField] private InfinityPlayerInfo myInfo;

    [Header("Offline for Feature")]
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private GameObject boardOffline;
    private int currentIndex;
    private void Start() {
        backButton.AddEvent(OnClose);
        btnPlay.AddEvent(OnPlayButtonClicked);
        signinGpsButton.AddEvent(OnSigninGps);
        inputInfo.AddOnClose(UpdateMyInfoUI);
        changeNameButton.AddEvent(() => {
            inputInfo.gameObject.SetActive(true);
            inputInfo.AddOnClose(UpdateMyInfoUI);
        });
        EventDispatcher.Instance.AddListener(EventKey.OnEnergyChanged, UpdateButtonState);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnEnergyChanged, UpdateButtonState);
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
        ToolbarScaler.Instance.SetActive(false);
    }
    private void UpdateUI() {
        inputInfo.gameObject.SetActive(GameResources.Instance.UserProfile.MyInfo != null && GameResources.Instance.UserProfile.MyInfo.PlayerName == "");
        ItemStack energyNeed = GameResources.Instance.InfinityModeData.EnergyNeed;
        ItemStack energy = GameResources.Instance.Inventory.GetItem(energyNeed.Id);
        SetEnergyView(energyNeed, true);
        bool canPlay = energy.Amount >= energyNeed.Amount;
        SetPlayButtonState(canPlay, true);
        UpdateMyInfoUI();
        UpdateBoard();
    }
    private void UpdateBoard() {
        bool internet = Networks.IsInternetAvaiable;
        boardNoInternet.SetActive(!internet);
        board.SetActive(internet && PrefSaver.PlayAsAccount);
        boardSignin.SetActive(internet && !PrefSaver.PlayAsAccount);
        boardOffline.SetActive(false);
        //boardOffline.SetActive(true);
        //board.SetActive(false);
        //boardNoInternet.SetActive(false);
        //boardSignin.SetActive(false);
    }
    private void UpdateButtonState() {
        ItemStack energyNeed = GameResources.Instance.InfinityModeData.EnergyNeed;
        ItemStack energy = GameResources.Instance.Inventory.GetItem(energyNeed.Id);
        bool canPlay = energy.Amount >= energyNeed.Amount;
        SetPlayButtonState(canPlay, true);
    }
    private void UpdateMyInfoUI() {
        var user = GameResources.Instance.UserProfile;
        var userInfo = user.MyInfo;
        var data = user.Data;
        if (userInfo != null) {
            playerName.text = $"{userInfo.PlayerName}";
            myInfo.UpdateUI(userInfo.PlayerRank, userInfo.PlayerName, userInfo.PlayerLevel, userInfo.PlayerScore);
            playerScore.text = $"{ userInfo.PlayerScore}";
        }
#if CHEAT || SAVEDATA
        if (data == null || data.Count == 0) {
            Debug.LogError("Data null");
            return;
        }
        for (int i = 0; i < playerInfos.Count; i++) {
            playerInfos[i].Initialize(data[i]);
        }
#endif
    }
    private void OnSigninGps() {
        PopupHUD.Instance.Show<SettingPopup>().AddOnClose(UpdateUI);
    }
    private void OnPlayButtonClicked() {
        ItemStack energyNeed = GameResources.Instance.InfinityModeData.EnergyNeed;
        ItemStack energy = GameResources.Instance.Inventory.GetItem(energyNeed.Id);
        SetEnergyView(energyNeed, true);
        bool canPlay = energy.Amount >= energyNeed.Amount;
        if (canPlay) {
            GameResources.Instance.Inventory.Remove(energyNeed.Id, energyNeed.Amount);
            IngameData.currentZoneIndex = 0;
            IngameData.PlayGame(GameMode.Infinity);
        }
    }
    private void SetPlayButtonState(bool interaction, bool show) {
        if (btnPlay) {
            btnPlay.gameObject.SetActive(show);
            if (show) {
                btnPlay.SetState(interaction);
            }
        }
    }

    public void SetEnergyView(ItemStack item, bool show) {
        if (energyPlayView) {
            energyPlayView.gameObject.SetActive(show);
            if (show) {
                energyPlayView.SetModel(item).Show();
            }
        }
    }
    private void OnClose() {
        Hide(() => ToolbarScaler.Instance.SetActive(true));
    }
    public override Frame OnBack() {
        ToolbarScaler.Instance.SetActive(true);
        return base.OnBack();
    }
    public override Frame SetAnimShow(bool leftToRight) {
        showAnimation = leftToRight ? showLeftToRight : showRightToLeft;
        return this;
    }
}
