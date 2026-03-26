using System.Collections.Generic;
using UnityEngine;
using GameSystem.Common.UI;
using System;
using DG.Tweening;

public class PausePopup : DOTweenFrame {
    [SerializeField] private ButtonBase btnResume;
    [SerializeField] private ButtonBase btnHome;
    [SerializeField] private ButtonBase btnSound;
    [SerializeField] private ButtonBase btnSetting;
    [SerializeField] private ButtonBase btnVibrate;

    [SerializeField] private Transform settingGO;
    [SerializeField] private Transform vibrateGO;
    [SerializeField] private Transform soundGO;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject vibrateOn;
    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private GameObject frameNotModYet;
    [SerializeField, Range(0f, 1f)] private float timeMove = 0.5f;

    [SerializeField] private PauseModCollectionDisplayer modCollectionDisplayer;

    private bool isClickSetting;
    private void Start() {
        btnResume.AddEvent(OnResumeButtonClicked);
        btnHome.AddEvent(OnHomeButtonClicked);
        btnSound.AddEvent(OnSoundButtonClicked);
        btnSetting.AddEvent(OnSettingButtonClicked);
        btnVibrate.AddEvent(OnVibrateButtonClicked);
        UpdateUISound(SoundManager.Instance.SoundEffectEnable);
        UpdateUIVibrate(SoundManager.Instance.VibrateEnable, false);
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        GameManager.Instance.Pause();
        isClickSetting = true;
        vibrateGO.position = settingGO.position;
        soundGO.position = settingGO.position;
        HUDManager.IgnoreUserInput(false);
        ShowMods();
        ChangeFrameNotModYetStatus();
    }


    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        if (PopupHUD.Instance.GetActiveFrame<ChooseModPopup>() == null
            && IngameHUD.Instance.GetActiveFrame<AngleOfferPopup>() == null)
            GameManager.Instance.Resume();
    }

    private void ShowMods() {
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (ship) {
            List<ModData> mods = ship.ShipSkill.Mods;
            modCollectionDisplayer.SetCapacity(mods.Count).SetItems(mods).Show();
        }
    }
    private void ChangeFrameNotModYetStatus() {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship != null && ship.ShipSkill != null)
            frameNotModYet.SetActive(ship.ShipSkill.Mods.Count == 0);
    }

    private void OnResumeButtonClicked() {
        OnBack();
    }

    private void OnHomeButtonClicked() {
        PopupHUD.Instance.ShowConfirm(() => {
            Time.timeScale = 1;
            var ship = GameManager.Instance.GameLoader.Ship;
            if (ship != null && ship.ShipAttack != null)
                ship.ShipAttack.ChangeStateShot(false);
            SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn, () => GameManager.Instance.QuitGame());
        }, OnResumeButtonClicked, "Are you sure?", "All this run's progress will be lost!", "QUIT", "CONTINUE", true, true);

    }
    private void OnSettingButtonClicked() {
        var tweenn = settingGO.DOLocalRotate(Vector3.forward * 180, timeMove / 2).SetLoops(-1, LoopType.Incremental).SetUpdate(true).SetEase(Ease.Linear);
        if (isClickSetting) {
            vibrateGO.DOMoveY(settingGO.position.y + 2, timeMove).SetUpdate(true);
            soundGO.DOMoveY(settingGO.position.y + 4, timeMove).SetUpdate(true).OnComplete(() => tweenn.Kill(false));
        }
        else {
            vibrateGO.DOMoveY(settingGO.position.y, timeMove).SetUpdate(true);
            soundGO.DOMoveY(settingGO.position.y, timeMove).SetUpdate(true).OnComplete(() => tweenn.Kill(false));
        }
        isClickSetting = !isClickSetting;
    }

    #region Sound, Vibrate
    private void OnSoundButtonClicked() {
        var sound = !SoundManager.Instance.SoundEffectEnable;
        SoundManager.Instance.SoundEffectEnable = sound;
        SoundManager.Instance.BackgroundMusicEnable = sound;
        UpdateUISound(sound);
        //SoundManager.Instance.StopBackgroundMusic();
    }
    private void UpdateUISound(bool turnOn) {
        soundOn.SetActive(turnOn);
        soundOff.SetActive(!turnOn);
    }
    private void OnVibrateButtonClicked() {
        var vibrate = !SoundManager.Instance.VibrateEnable;
        SoundManager.Instance.VibrateEnable = vibrate;
        UpdateUIVibrate(vibrate);
    }
    private void UpdateUIVibrate(bool turnOn, bool onClick = true) {
        vibrateOn.SetActive(turnOn);
        vibrateOff.SetActive(!turnOn);
        if (onClick && turnOn)
            Handheld.Vibrate();
    }
    public override Frame OnBack() {
        Hide();
        return this;
    }
    #endregion
}
