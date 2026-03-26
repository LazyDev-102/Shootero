using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gemmob;
using GameSystem.Common.UI;

public class SettingPopup : BasePopup {
    #region Variable
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button vibrateButton;
    [SerializeField] private Button noticeButton;
    [SerializeField] private ButtonExplorer giftCodeButton;
    [SerializeField] private ButtonExplorer restorePurchasedButton;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject musicOff;
    [SerializeField] private GameObject vibrateOn;
    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private GameObject noticeOn;
    [SerializeField] private GameObject noticeOff;
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private LockbarNotify lockbarNotify;

    [Header("Login")]
    [SerializeField] private ButtonExplorer loginButton;
    [SerializeField] private ButtonExplorer logOutButton;
    [SerializeField] private TextMeshProUGUI loginBtnText, logoutBtnText;
    [SerializeField] private Sprite gameCenterSprite;
    [SerializeField] private GameObject logoutGroupIcon;

    [SerializeField] private ButtonExplorer saveUser;
    [SerializeField] private TMP_InputField userName;

    private System.Action onClose;
    #endregion

    #region Constructors
    private void Awake() {
        soundButton.onClick.AddListener(OnClickSound);
        musicButton.onClick.AddListener(OnClickMusic);
        vibrateButton.onClick.AddListener(OnClickVibrate);
        noticeButton.onClick.AddListener(OnClickNotification);
        closeButton.AddEvent(OnClose);
        loginButton.AddEvent(Login);
#if !UNITY_IOS
        logOutButton.AddEvent(Logout);
#endif
        saveUser.AddEvent(SaveUser);

        EventDispatcher.Instance.AddListener(EventKey.OnLoginGPSFinish, UpdateLoginUI);

#if UNITY_IOS
        restorePurchasedButton.AddEvent(OnRestorePurchased);
        loginBtnText.text = "Sign in by Game Center";
        logoutBtnText.text = "Signed in with Game Center";
        logoutGroupIcon.SetActive(false);
        loginButton.SetIconSprite(gameCenterSprite);
        logOutButton.SetIconSprite(gameCenterSprite);
#endif

    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnLoginGPSFinish, UpdateLoginUI);
    }
    private void OnEnable() {
        Init();
    }
    public void AddOnClose(System.Action onClose) {
        this.onClose = onClose;
    }
    private void Init() {
        UpdateUISound(SoundManager.Instance.SoundEffectEnable);
        UpdateUIMusic(SoundManager.Instance.BackgroundMusicEnable);
        UpdateUIVibrate(false, false);
        UpdateLoginUI();
        versionText.text = $"version {Application.version}";
    }
#endregion

#region Sound
    private void OnClickSound() {
        var sound = !SoundManager.Instance.SoundEffectEnable;
        SoundManager.Instance.SoundEffectEnable = sound;
        SoundManager.Instance.BackgroundMusicEnable = sound;
        UpdateUISound(sound);
    }
    private void UpdateUISound(bool turnOn) {
        soundOn.SetActive(turnOn);
        soundOff.SetActive(!turnOn);
    }
#endregion

#region Music
    private void OnClickMusic() {
        var music = !SoundManager.Instance.BackgroundMusicEnable;
        SoundManager.Instance.BackgroundMusicEnable = music;
        UpdateUIMusic(music);
    }
    private void UpdateUIMusic(bool turnOn) {
        musicOn.SetActive(turnOn);
        musicOff.SetActive(!turnOn);
    }
#endregion

#region Vibrate
    private void OnClickVibrate() {
        var vibrate = !SoundManager.Instance.VibrateEnable;
        SoundManager.Instance.VibrateEnable = vibrate;
        UpdateUIVibrate(vibrate);
    }
    private void UpdateUIVibrate(bool turnOn, bool onClick = true) {
        vibrateOn.SetActive(turnOn);
        vibrateOff.SetActive(!turnOn);
        if (turnOn && onClick)
            Handheld.Vibrate();
    }
#endregion

#region Notification
    private void OnClickNotification() {
        var notice = !SoundManager.Instance.NotificationEnable;
        SoundManager.Instance.NotificationEnable = notice;
        UpdateUINotification(notice);
    }
    private void UpdateUINotification(bool turnOn) {
        noticeOn.SetActive(turnOn);
        noticeOff.SetActive(!turnOn);
    }
    private void OnClose() {
        onClose?.Invoke();
        onClose = null;
        Hide();
    }
#endregion

#region Login
    private void Login() {
        if (Networks.IsInternetAvaiable) {
            HUDManager.IgnoreUserInput(true);
            //UpdateLoginUI(false);
#if SAVEDATA
            GameLogin.Instance.LoginGPSFromSetting();
#else
            GameLogin.Instance.LoginGpsCheatFromSetting();
#endif
        }
        else {
            lockbarNotify.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
        }
    }
    private void Logout() {
        if (Networks.IsInternetAvaiable) {
            HUDManager.IgnoreUserInput(true);
            UpdateLoginUI(true);
            GameLogin.Instance.Logout(UpdateLoginUI, UpdateLoginUI);
        }
        else {
            lockbarNotify.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
        }
    }
    private void UpdateLoginUI() {
#if SAVEDATA
        saveUser.gameObject.SetActive(false);
        userName.gameObject.SetActive(false);
#endif
        bool isSigned = PrefSaver.PlayAsAccount;
        loginButton.gameObject.SetActive(!isSigned);
        logOutButton.gameObject.SetActive(isSigned);
        loginButton.interactable = !isSigned;
        logOutButton.interactable = isSigned;
        HUDManager.IgnoreUserInput(false);
    }
    private void UpdateLoginUI(bool force) {
#if SAVEDATA
        saveUser.gameObject.SetActive(false);
        userName.gameObject.SetActive(false);
#endif
        loginButton.gameObject.SetActive(force);
        logOutButton.gameObject.SetActive(!force);
        loginButton.interactable = force;
        logOutButton.interactable = !force;
    }

    private void SaveUser() {
        if (userName.text.Trim() == "") {
            lockbarNotify.SetContent("UserName not null!", 0.5f).Show();
        }
        else {
            PlayerPrefs.SetString("loginusertest", "usertest" + userName.text.Trim());
        }
    }
#endregion

    private void OnRestorePurchased() {
        if (!GameIAP.Initialized)
            return;
        GameIAP.Instance.RestorePurchases(CheckRestorePurchased);
    }
    private void CheckRestorePurchased() {
        GameResources.Instance.ShipPackData.RestorePurchased();
        lockbarNotify.SetContent("Restore successful!", 0.5f).Show();
    }
}
