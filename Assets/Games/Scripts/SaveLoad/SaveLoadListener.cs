using System;
using UnityEngine;

public class SaveLoadListener : MonoBehaviour {
    private void OnApplicationPause(bool pause) {
        if (!SaveLoad.Isnitialized) {
            return;
        }
        if (pause) {
            CheckShowPauseIngame();
        }
    }
    private void OnApplicationFocus(bool focus) {
        if (!SaveLoad.Isnitialized) {
            return;
        }
        if (!focus) {
            SaveLoad.Save();
            DateTime today = DateTime.Today;
            PrefSaver.Instance.SetLastDayPlay(today);
        }
    }

    private void OnApplicationQuit() {
        if (!SaveLoad.Isnitialized) {
            return;
        }
        SaveLoad.Save();
        DateTime today = DateTime.Today;
        PrefSaver.Instance.SetLastDayPlay(today);
    }
    private void CheckShowPauseIngame() {
        if (GameManager.Initialized) {
            var ship = GameManager.Instance.GameLoader.Ship;
            if (ship != null && !ship.IsDie() && PopupHUD.Instance.GetActiveFrame<RevivePopup>() == null && PopupHUD.Instance.GetActiveFrame<ChooseModPopup>() == null)
                PopupHUD.Instance.Show<PausePopup>();
        }
    }
}
