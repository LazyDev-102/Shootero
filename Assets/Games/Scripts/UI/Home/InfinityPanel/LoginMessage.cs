using GameSystem.Common.UI;
using System;
using UnityEngine;

public class LoginMessage : DOTweenFrame {
    [SerializeField] private ButtonBase confirmButton;
    [SerializeField] private ButtonBase cancelButton;

    private Action onConfirm;
    private Action onCancel;
    private void Awake() {
        confirmButton.AddEvent(OnOverrideCloudData);
        cancelButton.AddEvent(OnOverrideLocalData);
    }
    public void Initialize(Action onConfirm, Action onCancel) {
        this.onConfirm = onConfirm;
        this.onCancel = onCancel;
        gameObject.SetActive(true);
    }
    private void OnOverrideCloudData() {
        onConfirm?.Invoke();
        //DG.Tweening.DOVirtual.DelayedCall(1f, () => SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn));
        Close();
    }
    private void OnOverrideLocalData() {
        onCancel?.Invoke();
        Close();
    }
    private void Close() {
        gameObject.SetActive(false);
    }
}
