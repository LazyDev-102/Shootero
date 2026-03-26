using DG.Tweening;
using Gemmob;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#elif UNITY_IOS
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
#endif
using SimpleJSON;
using System;
using System.Collections;
using UnityEngine;

public class GameLogin : SingletonBind<GameLogin> {
    [SerializeField] private GameLoginEffect loginEffect;
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private ButtonBase loginButton;
    [SerializeField] private ButtonBase loginButtonForIos;
    [SerializeField] private ButtonBase signinGuest;
    [SerializeField] private string idTest;
    [SerializeField] private LockbarNotify lockbar;

    //private static PlayGamesPlatform platform;

    private void Awake() {
#if UNITY_ANDROID
        loginButton.gameObject.SetActive(true);
#elif UNITY_IOS
        loginButtonForIos.gameObject.SetActive(true);
#endif
        Time.timeScale = 1f;
        AddEvent();
        InitPlayGamesPlatform();
    }
    private void Start() {
        lockbar.gameObject.SetActive(false);
        lockbar.SetOriginPos(signinGuest.transform.position);
        if (PrefSaver.FirstOpenGameAfterConvert) {
            loginEffect.PreloadEffect();
            loginEffect.IsPlayEffect = true;
        }
        else {
            loginButton.SetState(false);
            loginButtonForIos.SetState(false);
            signinGuest.SetState(false);
            loginEffect.IsPlayEffect = false;
            if (!Networks.IsInternetAvaiable) {
                LoginGuest();
                return;
            }
            if (!PrefSaver.PlayAsAccount && !PrefSaver.RegisterAccount) {
                LoginGuest();
                return;
            }
#if SAVEDATA
            LoginPlatform();
#else
            LoginGPSCheat();
#endif
        }
    }

    private void AddEvent() {
#if SAVEDATA
        loginButton.AddEvent(LoginPlatform);
        loginButtonForIos.AddEvent(LoginPlatform);
#else
        loginButton.AddEvent(LoginGPSCheat);
        loginButtonForIos.AddEvent(LoginGPSCheat);
#endif
        signinGuest.AddEvent(LoginGuest);

        EventDispatcher.Instance.AddListener(EventKey.OnLoadHomeScene, DownloadLeaderboard);
    }
    protected override void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnLoadHomeScene, DownloadLeaderboard);
    }

    private void InitPlayGamesPlatform() {
#if UNITY_ANDROID
        if (Networks.IsInternetAvaiable) {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                                                                                  .Build();
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.InitializeInstance(config);
            //platform = PlayGamesPlatform.Activate();
            PlayGamesPlatform.Activate();
        }
#endif
    }

    public void LoginPlatform() {
        if (Networks.IsInternetAvaiable) {
#if UNITY_ANDROID
            if (PlayGamesPlatform.Instance != null) {
                loginButton.SetState(false);
                PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, ((SignInStatus status) => {
                    ProcessOnAuthenticated(status == SignInStatus.Success);
                }));
            }
            else {
                InitPlayGamesPlatform();
                lockbar.SetContent("Wait for Google Play Games initialize!", 0.5f).Show();
            }
#elif UNITY_IOS
            loginButtonForIos.SetState(false);
            Social.localUser.Authenticate(ProcessOnAuthenticated);
#endif
        }
        else {
            if (!PrefSaver.FirstOpenGameAfterConvert) {
                PrefSaver.PlayAsAccount = false;
                ActionOnSignIn();
            }
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
        }
    }

    private void ProcessOnAuthenticated(bool success)
    {
        if (success)
        {
            lockbar.SetContent(GameDefine.LoginSuccess, 0.5f).Show();
            if (GetUserId() != GetUidLocal())
            {
                DownloadUserData(DownloadUserDataSuccess);
            }
            else
            {
                PrefSaver.PlayAsAccount = true;
                ActionOnSignIn();
            }
        }
        else
        {
            lockbar.SetContent(GameDefine.LoginFail, 0.5f).Show();
            if (!PrefSaver.FirstOpenGameAfterConvert)
            {
                PrefSaver.PlayAsAccount = false;
                ActionOnSignIn();
            }
        }
    }

    public void LoginGPSCheat() {
        if (Networks.IsInternetAvaiable) {
            loginButton.SetState(false);
            loginButtonForIos.SetState(false);
            lockbar.SetContent(GameDefine.LoginSuccess, 0.5f).Show();
            if (GetUserId() != GetUidLocal()) {
                DownloadUserData(DownloadUserDataSuccess);
            }
            else {
                PrefSaver.PlayAsAccount = true;
                ActionOnSignIn();
            }
        }
        else {
            if (!PrefSaver.FirstOpenGameAfterConvert) {
                PrefSaver.PlayAsAccount = false;
                ActionOnSignIn();
            }
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
        }
    }

    public void LoginGPSFromSetting() {
        if (Networks.IsInternetAvaiable) {
#if UNITY_ANDROID
            if (PlayGamesPlatform.Instance != null) {
                PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, ((SignInStatus status) => {
                    if (status == SignInStatus.Success) {
                        DownloadUserData(DownloadUserDataSusscessWithSetting);
                    }
                    else {
                        NotificationText.Instance.Show(GameDefine.LoginFail, NotificationText.NoticeType.Error);
                        EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);
                    }
                }));
            }
            else {
                InitPlayGamesPlatform();
                NotificationText.Instance.Show("Wait for Google Play Games initialize!", NotificationText.NoticeType.Error);
                EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);
            }
#elif UNITY_IOS
            Social.localUser.Authenticate((success)=> {
                if (success)
                {
                    DownloadUserData(DownloadUserDataSusscessWithSetting);
                }
                else
                {
                    NotificationText.Instance.Show(GameDefine.LoginFail, NotificationText.NoticeType.Error);
                    EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);
                }
            });
#endif
        }
        else {
            NotificationText.Instance.Show(GameDefine.InternetDisconnected, NotificationText.NoticeType.Error);
            EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);
        }
    }
    public void LoginGpsCheatFromSetting() {
        if (Networks.IsInternetAvaiable) {
            NotificationText.Instance.Show(GameDefine.LoginSuccess, NotificationText.NoticeType.Info);
            DownloadUserData(DownloadUserDataSusscessWithSetting);
        }
        else {
            NotificationText.Instance.Show(GameDefine.InternetDisconnected, NotificationText.NoticeType.Info);
            EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);
        }
    }

    public void Logout(Action onCompleted, Action onfail = null) {
        SaveLoad.Save();
        UploadUserData((data) => {
            PrefSaver.PlayAsAccount = false;
#if UNITY_ANDROID
            PlayGamesPlatform.Instance.SignOut();
#elif UNITY_IOS
            //do nothing, cant sign out on ios
#endif
            onCompleted?.Invoke();
        }, onfail);
        UploadLeaderBoardData();
    }

    private void LoginGuest() {
        PrefSaver.PlayAsAccount = false;
        signinGuest.SetState(false);
        ActionOnSignIn();
    }

    private void ActionOnSignIn() {
        loginEffect.IsPlayEffect = false;
        loginButton.gameObject.SetActive(false);
        loginButtonForIos.gameObject.SetActive(false);
        signinGuest.gameObject.SetActive(false);
        SceneLoader.Instance.FakeScene();
        DOVirtual.DelayedCall(.5f, () => {
            myCanvas.enabled = false;
            GameResourceLoader.Instance.LoadAllData();
            ShowNextScene();
        });
    }

    private void ShowNextScene() {
        //SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.ConfigAds);
        //PlayerStatManager.Instance.Preload();
        //return;
        if (GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce) {
            SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.Home, onFadeOut: () =>
             EventDispatcher.Instance.Dispatch(EventKey.OnLoadHomeScene));
        }
        else {
            //hotfix: not a proud solution but it works
            GameResources.Instance.EnergyData.ResetAllRemain();
            SaveLoad.SaveEnergyData();
            SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.Tutorial);
        }
        PlayerStatManager.Instance.Preload();
    }

#region Get User Info
    public string GetUserName() {
#if !SAVEDATA
        return "NameEditByPhapND";
#elif SAVEDATA && UNITY_ANDROID
        return PlayGamesPlatform.Instance.localUser.authenticated ? PlayGamesPlatform.Instance.localUser.userName : string.Empty;
#elif SAVEDATA && UNITY_IOS
        return Social.localUser.authenticated ? Social.localUser.userName : string.Empty;
#endif

    }

    public string GetUserEmail() {
#if SAVEDATA && UNITY_ANDROID
        return ((PlayGamesLocalUser)PlayGamesPlatform.Instance.localUser).Email;
#else
        return "EmailEditByPhapND";
#endif
    }

    public string GetUserId() {
#if SAVEDATA && UNITY_ANDROID
        return PlayGamesPlatform.Instance.localUser.authenticated ? PlayGamesPlatform.Instance.localUser.id : String.Empty;
#elif SAVEDATA && UNITY_IOS
        return Social.localUser.authenticated ? Social.localUser.id : string.Empty;
#else
        return SystemInfo.deviceUniqueIdentifier;
        //return PlayerPrefs.GetString("loginusertest", "usertest1");
        if (PrefSaver.ConvertedData)
            return PlayerPrefs.GetString("loginusertest", "usertest1");
        else
            return "usertest_" + UnityEngine.Random.Range(1000, 2000);
#endif
    }
    #endregion

    #region Upload User Profile
    private void UploadUserProfile(Action<JSONNode> onSuccessed) {
        if (Networks.IsInternetAvaiable) {
            var user = GameResources.Instance.UserProfile;
            WWWForm form = new WWWForm();
            form.AddField(APIConfig.uid, GetUserId());
            form.AddField(APIConfig.NameIngame, user.GetIngameName());
            form.AddField(APIConfig.Score, user.GetHighScore());
            form.AddField(APIConfig.Level, user.GetLevel());
            form.AddField(APIConfig.Token, user.GetInfo());

            StartCoroutine(PostData(form, GameURL.UserData.UploadUserProfile, onSuccessed, null));
        }
        else
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
    }
    private void UploadUserProfileDataSuccess(JSONNode data) {
        PrefSaver.RegisterAccount = true;
        HandleTextFile.WriteString(GameURL.UserIdPath, GetUserId());
    }
#endregion

#region Download User Profile
    private void DownloadUserProfile() {
        if (Networks.IsInternetAvaiable) {
            var user = GameResources.Instance.UserProfile;
            WWWForm form = new WWWForm();
            form.AddField(APIConfig.uid, GetUserId());

            StartCoroutine(PostData(form, GameURL.UserData.UploadUserProfile, DownloadUserProfileDataSuccess, null));
        }
        else
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
    }
    private void DownloadUserProfileDataSuccess(JSONNode data) {

    }
#endregion

#region Upload User Data
    public void UploadUserData(Action<JSONNode> onSuccessed, Action onfail = null) {
        if (Networks.IsInternetAvaiable) {
            WWWForm form = new WWWForm();
            form.AddField(APIConfig.uid, GetUserId());
            form.AddField(APIConfig.Version, Application.version);
            form.AddField(APIConfig.data, SaveLoad.LoadLocalData());
            StartCoroutine(PostData(form, GameURL.UserData.UploadData, onSuccessed, onfail));
        }
        else
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
    }
    private void UploadUserDataSuccess(JSONNode data) {
        //JSONNode node = JSONNode.Parse(data);
        //string uid = node[JsonKey.UserID];
        //int status = node[JsonKey.Status].AsInt;
        //if (status != -1) {
        //}
    }
#endregion

#region Download User Data
    private void DownloadUserData(Action<JSONNode> onSuccess) {
        if (Networks.IsInternetAvaiable) {
            var user = GameResources.Instance.UserProfile;
            WWWForm form = new WWWForm();
            form.AddField(APIConfig.uid, GetUserId());
            StartCoroutine(PostData(form, GameURL.UserData.DownloadData, onSuccess, null));
        }
        else
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
    }

    private void DownloadUserDataSuccess(JSONNode data) {
        PrefSaver.RegisterAccount = true;
        PrefSaver.PlayAsAccount = true;
        HandleTextFile.WriteString(GameURL.UserIdPath, GetUserId());
        JSONNode node = JSONNode.Parse(data);
        int status = node[JsonKey.Status].AsInt;
        if (status == 1) {
            string content = node[JsonKey.Data][JsonKey.Data].ToString();
            SaveLoad.SaveLocalData(content);
        }
        else {
            SaveLoad.SaveLocalData("");
            UploadUserProfile(UploadUserProfileDataSuccess);
            lockbar.SetContent("Haven't user in server. Upload user profile!", 0.5f).Show();
        }

        ActionOnSignIn();
    }
    private void DownloadUserDataSusscessWithSetting(JSONNode data) {
        PrefSaver.PlayAsAccount = true;
        JSONNode node = JSONNode.Parse(data);
        int status = node[JsonKey.Status].AsInt;
        EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);

        //So sanh tai khoan
        if (GetUserId().Equals(GetUidLocal())) {
            NotificationText.Instance.Show(GameDefine.LoginSuccess, NotificationText.NoticeType.Info);
            UploadUserData(UploadUserDataSuccess);
        }
        else {
            if (!PrefSaver.RegisterAccount) {
                SaveLoad.Save();
                UploadUserProfile(UploadUserProfileDataSuccess);
                return;
            }
            PopupHUD.Instance.Show<LoginMessage>().Initialize(() => {
                // Load data from Server
                string content = "";
                if (status != 1) {
                    SaveLoad.SaveLocalData("");
                    UploadUserProfile(UploadUserProfileDataSuccess);
                    NotificationText.Instance.Show("Haven't user in server. Upload user profile!", NotificationText.NoticeType.Info, true);
                }
                else {
                    content = node[JsonKey.Data][JsonKey.Data].ToString();
                    SaveLoad.SaveLocalData(content);
                }
                HandleTextFile.WriteString(GameURL.UserIdPath, GetUserId());
                GameResources.Instance.Reload();
                ActionOnSignIn();
            }, () => {
                // Logout
                PrefSaver.PlayAsAccount = false;
                Logout(null);
                NotificationText.Instance.Show(GameDefine.LoginFail, NotificationText.NoticeType.Error, true);
                EventDispatcher.Instance.Dispatch(EventKey.OnLoginGPSFinish);
            });
        }
    }
    private string GetUidLocal() {
        HandleTextFile.ReadString(GameURL.UserIdPath, out string uid);
        return uid;
    }
#endregion

#region Upload Leaderboard Data
    public void UploadLeaderBoardData() {
        if (Networks.IsInternetAvaiable && PrefSaver.PlayAsAccount) {
            var user = GameResources.Instance.UserProfile;
            WWWForm form = new WWWForm();
            form.AddField(APIConfig.uid, GetUserId());
            form.AddField(APIConfig.NameIngame, user.GetIngameName());
            form.AddField(APIConfig.Score, user.GetHighScore());
            form.AddField(APIConfig.Level, user.GetLevel());

            StartCoroutine(PostData(form, GameURL.Leaderboard.UploadData, UploadLeaderboardSuccess, null));
        }
        else
            lockbar.SetContent(GameDefine.InternetDisconnected, 0.5f).Show();
    }
    private void UploadLeaderboardSuccess(JSONNode data) {
        //GameResources.Instance.UserProfile.LoadFJson(data);
    }
#endregion

#region Download Leaderboard Data
    public void DownloadLeaderboard() {
        if (Networks.IsInternetAvaiable && PrefSaver.PlayAsAccount) {
            WWWForm form = new WWWForm();
            form.AddField(APIConfig.uid, GetUserId());

            StartCoroutine(PostData(form, GameURL.Leaderboard.DownloadData, DownloadDataLeaderboardSuccess, null));
        }
    }
    private void DownloadDataLeaderboardSuccess(JSONNode data) {
        var userProfile = GameResources.Instance.UserProfile;
        JSONNode newData = JSONNode.Parse(data);
        JSONNode info = newData[JsonKey.Data];
        if (info != null) {
            int rank = info["ranking"].AsInt;
            userProfile.SetRank(rank);
            JSONArray content = info["rankData"].AsArray;
            var rankInfo = userProfile.Data;
            int maxRank = rankInfo.Count;
            for (int i = 0; i < content.Count; i++) {
                if (maxRank <= i)
                    continue;
                rankInfo[i].PlayerRank = content[i]["rank"].AsInt;
                rankInfo[i].PlayerName = content[i]["name"].Value;
                rankInfo[i].PlayerLevel = content[i]["level"].AsInt;
                rankInfo[i].PlayerScore = content[i]["point"].AsInt;
                rankInfo[i].PlayerLevel = rankInfo[i].PlayerLevel < 0 ? 0 : rankInfo[i].PlayerLevel > 99 ? 99 : rankInfo[i].PlayerLevel;
            }
        }
        else {
            Debug.LogError("Info null");
        }

    }
#endregion

    public IEnumerator PostData(WWWForm form, string url, Action<JSONNode> onSuccessed, Action onFailed) {
        using (var www = UnityEngine.Networking.UnityWebRequest.Post(url, form)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                onFailed?.Invoke();
            }
            else {
                string data = www.downloadHandler.text;
                onSuccessed?.Invoke(data);
            }
        }
    }
}